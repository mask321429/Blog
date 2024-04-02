using AutoMapper;
using Azure;
using Blog.Data;
using Blog.Data.Models;
using Blog.Services.Interfaces;
using Blog2023.Data.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Blog.Services
{

    public class AuthService : IAuthService
    {

        private ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public AuthService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        //....................................<Регистрация пользователя>..............................................................
        public async Task<TokenDTO> Register(UserRegisterDTO userRegisterDTO)
        {
            userRegisterDTO.Email = NormalizeAttribute(userRegisterDTO.Email);

            await CheckEmailIdentity(userRegisterDTO);

            CheckGender(userRegisterDTO.Gender);

            CheckBirthDate(userRegisterDTO.BirthDate);

            //Так как использовал чат, решил добавить коментариев, чтобы в дальнейшем закрепить. 
            // Генерация случайной последовательности байтов для соли
            byte[] saltBytes;
            RandomNumberGenerator.Fill(saltBytes = new byte[16]);

            // Создание объекта для хеширования пароля с использованием алгоритма PBKDF2
            using var deriveBytes = new Rfc2898DeriveBytes(userRegisterDTO.Password, saltBytes, 100000);

            // Получение хэша пароля в виде массива байтов
            byte[] passwordHashBytes = deriveBytes.GetBytes(20);

            // Объединение соли и хэша пароля в один массив байтов
            byte[] combinedBytes = new byte[36];
            Buffer.BlockCopy(saltBytes, 0, combinedBytes, 0, 16);
            Buffer.BlockCopy(passwordHashBytes, 0, combinedBytes, 16, 20);

            // Преобразование объединенных байтов в строку Base64 для сохранения в базе данных
            string savedPasswordHash = Convert.ToBase64String(combinedBytes);

            

            await _dbContext.Users.AddAsync(new User
            {
                Id = Guid.NewGuid(),
                FullName = userRegisterDTO.FullName,
                BirthDate = userRegisterDTO.BirthDate,
                Email = userRegisterDTO.Email,
                Gender = userRegisterDTO.Gender,
                Password = savedPasswordHash,
                PhoneNumber = userRegisterDTO.PhoneNumber,
            });
            await _dbContext.SaveChangesAsync();

            var ForSuccessfulLogin = new UserLoginDTO
            {
                Email = userRegisterDTO.Email,
                Password = userRegisterDTO.Password
            };

            return await Login(ForSuccessfulLogin);
        }


        //...........................................<Вход в аккаунт>.......................................................
        public async Task<TokenDTO> Login(UserLoginDTO ForSuccessfulLogin)
        {
            ForSuccessfulLogin.Email = NormalizeAttribute(ForSuccessfulLogin.Email);
            var identity = await GetIdentity(ForSuccessfulLogin.Email, ForSuccessfulLogin.Password);
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: TokenConfigurations.Issuer,
                audience: TokenConfigurations.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.AddMinutes(TokenConfigurations.Lifetime),
                signingCredentials: new SigningCredentials(TokenConfigurations.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256));

            var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var result = new TokenDTO()
            {
                Token = encodeJwt
            };

            return result;
        }
        //...........................................<Выход из аккаунт>.......................................................
        public async Task Logout(string token)
        {
            var alreadyExistsToken = await _dbContext.Tokens.FirstOrDefaultAsync(x => x.InvalidToken == token);

            if (alreadyExistsToken == null)
            {
                var handler = new JwtSecurityTokenHandler();
                var expiredDate = handler.ReadJwtToken(token).ValidTo;
                _dbContext.Tokens.Add(new Token { InvalidToken = token, ExpiredDate = expiredDate });
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                var exception = new Exception();
                exception.Data.Add(StatusCodes.Status401Unauthorized.ToString(),
                    "Token is already invalid"
                );
                throw exception;
            }
        }


        //..............................<Получение данных об аккаунте>............................................
        public async Task<UserDTO> GetInfoProfile(Guid userId)
        {
            var userEntity = await _dbContext
              .Users
              .FirstOrDefaultAsync(x => x.Id == userId);

            if (userEntity != null)
                return _mapper.Map<UserDTO>(userEntity);

            var exception = new Exception();
            exception.Data.Add(StatusCodes.Status401Unauthorized.ToString(),
                "User not exists"
            );
            throw exception;

        }

        //..............................<Изменение данных профиля>............................................
        public async Task EditProfile(Guid userId, userEditModel user)
        {
            var userEntity = await _dbContext
           .Users
           .FirstOrDefaultAsync(x => x.Id == userId);

            if (userEntity == null)
            {
                var exception = new Exception();
                exception.Data.Add(StatusCodes.Status401Unauthorized.ToString(),
                    "User not exists"
                );
                throw exception;
            }

            CheckGender(user.Gender);
            CheckBirthDate(user.BirthDate);

            userEntity.FullName = user.FullName;
            userEntity.BirthDate = user.BirthDate;
            userEntity.Gender = user.Gender;
            userEntity.PhoneNumber = user.PhoneNumber;

            await _dbContext.SaveChangesAsync();

        }


        //..............................<Удаление пробелов и верхнего регистра>............................................

        public static string NormalizeAttribute(string value)
        {
            return value.TrimEnd().ToLower();
        }

        //....................................<Проверка Пола>..............................................................
        private static void CheckGender(string gender)
        {
            if (gender == Gender.Male.ToString() || gender == Gender.Female.ToString()) return;

            var exception = new Exception();
            exception.Data.Add(StatusCodes.Status409Conflict.ToString(),
                $"Possible Gender values: {Gender.Male.ToString()}, {Gender.Female.ToString()}");
            throw exception;
        }


        //......................................<Проверка даты рождения>............................................................

        private static void CheckBirthDate(DateTime? birthDate)
        {
            if (birthDate == null || birthDate <= DateTime.Now) return;

            var exception = new Exception();
            exception.Data.Add(StatusCodes.Status409Conflict.ToString(),
                "Birth date can't be later than today");
            throw exception;
        }
        //...........................................<Проверка Email адреса>.......................................................


        private async Task CheckEmailIdentity(UserRegisterDTO userRegisterDTO)
        {
            var email = await _dbContext
           .Users
            .Where(x => userRegisterDTO.Email == x.Email)
            .FirstOrDefaultAsync();

            if (email != null)
            {
                var exception = new Exception();
                exception.Data.Add(StatusCodes.Status409Conflict.ToString(),
                $"Аккаунт с данным - '{userRegisterDTO.Email}' уже существует");
                throw exception;
            }
        }

        //..............................<т наличие пользователя с указанным email в базе данных и после проверка пароля на правильность>....................................................................






        private async Task<ClaimsIdentity> GetIdentity(string email, string password)
        {
            var userEntity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);

            if (userEntity == null)
            {
                throw new UnauthorizedAccessException("User does not exist.");
            }

            if (!ValidatePasswordHash(userEntity.Password, password))
            {
                throw new UnauthorizedAccessException("Incorrect password.");
            }

            var claims = new List<Claim>
    {
        new(ClaimsIdentity.DefaultNameClaimType, userEntity.Id.ToString())
    };

            return new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }




        //..............................<функция сравнивает хэш введенного пользователем пароля с хэшем, сохраненным в базе данных, для проверки правильности предоставленного пароля>....................................................................

        private static bool ValidatePasswordHash(string savedPasswordHash, string userEnteredPassword)
        {
            // Проверяем, если сохраненный хеш пароля пустой или null
            if (string.IsNullOrEmpty(savedPasswordHash))
            {
                return false; // Возможно, нужно более специфичное действие при отсутствии хеша
            }

            var hashBytes = Convert.FromBase64String(savedPasswordHash);

            // Проверяем, если длина байт хеша меньше ожидаемой длины (минимальная длина 36 байт, соли и хеша пароля)
            if (hashBytes.Length < 36)
            {
                return false; // Возможно, нужно более специфичное действие при неверной длине хеша
            }

            var storedSalt = new byte[16];
            Array.Copy(hashBytes, 0, storedSalt, 0, 16);

            var pbkdf2 = new Rfc2898DeriveBytes(userEnteredPassword, storedSalt, 100000);
            var computedHash = pbkdf2.GetBytes(20);

            // Сравниваем хеши паролей
            for (var i = 0; i < 20; i++)
            {
                if (hashBytes[i + 16] != computedHash[i])
                {
                    return false; // Если обнаружено несоответствие, возвращаем false
                }
            }

            return true; // Если все проверки пройдены успешно, возвращаем true
        }




    }
}
