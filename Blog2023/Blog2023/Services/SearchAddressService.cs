using Blog.Data;
using Blog.Services.Interfaces;
using Blog2023.Data.DTO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace Blog.Services
{

    public class SearchAddressService : IAddressService
    {
        private readonly ApplicationDbContextAddress _dbContext;


        public SearchAddressService(ApplicationDbContextAddress dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task<List<SearchAdressDTO>> SearchAdress(int? parentObjectId, string? query)
        {
            if (parentObjectId == null && query == null)
            {
                List<SearchAdressDTO> addressDTOs = await CheckFirsRequest(parentObjectId, query);
                if (addressDTOs == null)
                {
                    Console.WriteLine("Ошибка в первой функции");
                }
                return addressDTOs;
            }
            else if ((parentObjectId != null && query != null) || (parentObjectId != null))
            {
                List<SearchAdressDTO> addressDTOsWithValue = await CheckRequestWithValue(parentObjectId, query);
                Console.WriteLine("Ошибка 1");
                return addressDTOsWithValue;
            }

            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status401Unauthorized.ToString(), "No address found");
            throw ex;
        }

        public async Task<List<SearchAdressDTO>> CheckRequestWithValue(int? parentObjectId, string? query)
        {
            var searchAddressInHierarchy = await _dbContext.as_adm_hierarchy
                .Where(x => x.parentobjid == parentObjectId)
                .Select(x => x.objectid)
                .ToListAsync();

            var addressDTOs = new List<SearchAdressDTO>();
            int defaultValueForHouseType = 0;
            if (searchAddressInHierarchy != null && searchAddressInHierarchy.Any())
            {
                var searchAddressInObj = await _dbContext.as_addr_obj
                    .Where(c => searchAddressInHierarchy.Contains(c.objectid) && c.isactual == 1)
                    .Select(x => new SearchAdressDTO
                    {
                        objectId = x.objectid,
                        objectGuid = x.objectguid,
                        text =x.typename + ". " + x.name ,
                        objectLevel = GetObjectLevelText(x.level),
                        objectLevelText = GarAddressLevel(x.level)
                    })
                    .ToListAsync();

                var searchAddressInHouses = await _dbContext.as_houses
                    .Where(c => searchAddressInHierarchy.Contains(c.objectid) && c.isactual == 1)
                    .Select(houseAddress => new SearchAdressDTO
                    {
                        objectId = houseAddress.objectid,
                        objectGuid = houseAddress.objectguid,
                        text = houseAddress.housenum,
                        objectLevel = GarAddressLevelHome((houseAddress.housetype ?? defaultValueForHouseType).ToString()),
                        objectLevelText = GetAddressLevelHome((houseAddress.housetype ?? defaultValueForHouseType).ToString())

                    })
                    .ToListAsync();

                addressDTOs.AddRange(searchAddressInObj);
                addressDTOs.AddRange(searchAddressInHouses);

                if (!string.IsNullOrEmpty(query))
                {
                    return addressDTOs
                        .Where(g => Regex.IsMatch(g.text, query))
                        .Take(10)
                        .ToList();
                }
            }

            return addressDTOs.Take(10).ToList();
        }




        public async Task<List<SearchAdressDTO>> ChainAddress(Guid id)
        {
            List<SearchAdressDTO> address = new List<SearchAdressDTO>();

            var addressDto = await _dbContext.as_houses
                .Where(x => x.objectguid == id)
                .Join(_dbContext.as_adm_hierarchy, x => x.objectid, t => t.objectid, (x, t) => t.path)
                .FirstOrDefaultAsync();

            if (addressDto == null)
            {
                addressDto = await _dbContext.as_addr_obj
                    .Where(x => x.objectguid == id)
                    .Join(_dbContext.as_adm_hierarchy, x => x.objectid, t => t.objectid, (x, t) => t.path)
                    .FirstOrDefaultAsync();

                if (addressDto == null)
                {
                    return null;
                }
            }

            var link = addressDto.Split(".");

            foreach (var linkParent in link)
            {
                var searchAddress = await _dbContext.as_addr_obj
                    .Where(x => x.objectid.ToString() == linkParent)
                    .Select(x => new SearchAdressDTO
                    {
                        objectId = x.objectid,
                        objectGuid = x.objectguid,
                        text = x.typename + " " + x.name,
                        objectLevel = GetObjectLevelText(x.level),
                        objectLevelText = GarAddressLevel(x.level)
                    })
                    .FirstOrDefaultAsync();

                if (searchAddress == null)
                {
                   
                    searchAddress = await _dbContext.as_addr_obj
                        .Where(x => x.objectid.ToString() == linkParent)
                        .Select(x => new SearchAdressDTO
                        {
                            objectId = x.objectid,
                            objectGuid = x.objectguid,
                            text = x.typename + " " + x.name,
                            objectLevel = GetObjectLevelText(x.level),
                            objectLevelText = GarAddressLevel(x.level)
                        })
                        .FirstOrDefaultAsync();
                }

                if (searchAddress != null)
                {
                    address.Add(searchAddress);
                }
            }

            return address;
        }






        public async Task<List<SearchAdressDTO>> CheckFirsRequest(int? parentObjectId, string? query)
        {
            var firstRequest = await _dbContext.as_adm_hierarchy
                .Where(x => x.parentobjid == 0)
                .FirstOrDefaultAsync();

            if (firstRequest == null)
            {
                return new List<SearchAdressDTO>();
            }

            var pathFromDatabase = firstRequest.path;
            long longValue = long.Parse(pathFromDatabase);

            var searchAddress = await _dbContext.as_addr_obj
                .Where(x => x.objectid == longValue)
                .FirstOrDefaultAsync();

            if (searchAddress == null)
            {
                return new List<SearchAdressDTO>();

            }

            var addressDTOs = new SearchAdressDTO
            {
                objectId = searchAddress.objectid,
                objectGuid = searchAddress.objectguid,
                text = searchAddress.name,
                objectLevel = "Region",
                objectLevelText = "Субъект РФ"
            };

            return new List<SearchAdressDTO> { addressDTOs };
        }
        private static string GetObjectLevelText(string level)
        {
            switch (level)
            {
                case "1":
                    return "Cубъект РФ";
                case "2":
                    return "Административный район";
                case "3":
                    return "Муниципальный район";
                case "4":
                    return "Поселение";
                case "5":
                    return "Поселение";
                case "6":
                    return "Город";
                case "7":
                    return "Населенный пункт";
                case "8":
                    return "Элемент планировочной структуры";
                case "9":
                    return "Элемент цлично-дорожной сети";
                case "10":
                    return "Земельный участок";
                case "11":
                    return "Помещение в пределах здания";
                case "12":
                    return "Помещение в пределах помещения";
                case "13":
                    return "Автононная облась";
                default:
                    return "";
            }
        }
        private static string GarAddressLevel(string level)
        {
            switch (level)
            {
                case "1":
                    return "Region";
                case "2":
                    return "Administrative Area";
                case "3":
                    return "Municipal Area";
                case "4":
                    return "Rural Urban Settlement";
                case "5":
                    return "City";
                case "6":
                    return "Locality";
                case "7":
                    return "Element of Planning Structure";
                case "8":
                    return "Element of Road Network";
                case "9":
                    return "Land";
                case "10":
                    return "Building";
                case "11":
                    return "Room";
                case "12":
                    return "Room in Rooms";
                case "13":
                    return "Autonomous Region Level";
                case "14":
                    return "Intracity Level";
                case "15":
                    return "Additional Territories Level";
                case "16":
                    return "Level of Objects in Additional Territories";
                case "17":
                    return "Car Place";
                default:
                    return "";
            }
        }

        private static string GetAddressLevelHome(string level)
        {
            switch (level)
            {
                case "1":
                    return "Владение";
                case "2":
                    return "Дом";
                case "3":
                    return "Домовладение";
                case "4":
                    return "Гараж";
                case "5":
                    return "Здание";
                default:
                    return "";
            }
        }
        private static string GarAddressLevelHome(string level)
        {
            switch (level)
            {
                case "1":
                    return "Possession";
                case "2":
                    return "House";
                case "3":
                    return "Homeownership";
                case "4":
                    return "Garage";
                case "5":
                    return "Building";
                default:
                    return "";
            }
        }


    }
}