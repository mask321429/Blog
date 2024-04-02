using AutoMapper;
using Blog.Data;
using Blog.Data.DTO;
using Blog.Data.Models;
using Blog.Services.Interfaces;
using Blog2023.Data.DTO;
using Blog2023.Data.enums;
using Blog2023.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;



namespace Blog.Services
{

    public class CommunityService : ICommunityService 
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CommunityService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<CommunityDTO>> GetCommunity()
        {
            var communityEntities = await _dbContext.Community.ToListAsync();

            if (communityEntities != null && communityEntities.Any())
            {
                var communityDTOs = _mapper.Map<List<CommunityDTO>>(communityEntities);
                return communityDTOs;
            }

            // Если сообщества не найдены, вернуть пустой список
            return new List<CommunityDTO>();
        }


        public async Task<List<MyCommunityDTO>> GetMyCommunity(Guid userId)
        {
            var userCommunities = await _dbContext.MyCommunity
                .Where(mc => mc.UserId == userId)
                .ToListAsync();

            var myCommunityDTOs = userCommunities.Select(mc => new MyCommunityDTO
            {
                UserId = mc.UserId,
                CommunityId = mc.CommunityId,
                Role = mc.Role
            }).ToList();

            return myCommunityDTOs;
        }

        public async Task<CommunityDTO> GetCommunityId(Guid communityId)
        {
            var communityEntity = await _dbContext.Community
                .Include(c => c.Administrators)
                .FirstOrDefaultAsync(x => x.Id == communityId);

            if (communityEntity == null)
            {
                var exception = new Exception();
                exception.Data.Add(StatusCodes.Status404NotFound.ToString(), "Community entity not found");
                throw exception;
            }

            var communityDTO = _mapper.Map<CommunityDTO>(communityEntity);


            var administrators = communityEntity.Administrators.Select(admin => _mapper.Map<AdministratorDTO>(admin)).ToList();


            communityDTO.Administrators = administrators;

            return communityDTO;
        }
  

     
 
        public class PostListWithPagination
        {
            public List<PostDTO> Posts { get; set; }
            public PageInfoModel PaginationInfo { get; set; }
        }

        public async Task<object> GetPostList(GetPostListQuery listQuery)
        {
            int skipAmount = (listQuery.Page - 1) * listQuery.SizePage;

            var postСommunityEntity = await _dbContext.Posts
                .Where(c => c.CommunityId == listQuery.Id)
                .Skip(skipAmount)
                .Take(listQuery.SizePage)
                .ToListAsync();

            if (postСommunityEntity == null || !postСommunityEntity.Any())
            {
                return new object[] { new PostListWithPagination { Posts = new List<PostDTO>(), PaginationInfo = new PageInfoModel { Size = listQuery.SizePage, Count = 0, Current = listQuery.Page } } };
            }

            var postDTOs = FilterPosts(listQuery, postСommunityEntity);

            var totalCount = await _dbContext.Posts.CountAsync(c => c.CommunityId == listQuery.Id);

            var postListWithPagination = new PostListWithPagination
            {
                Posts = postDTOs,
                PaginationInfo = new PageInfoModel { Size = listQuery.SizePage, Count = totalCount, Current = listQuery.Page }
            };

            return new object[] { postListWithPagination };
        }

        private List<PostDTO> FilterPosts(GetPostListQuery postListQuery, List<PostModel> postList)
        {
            string orderBy = postListQuery.sortType.ToString(); 

            switch (orderBy)
            {
                case "CreateAsc":
                    return postList.OrderBy(s => s.CreateTime)
                                   .Select(post => MapToPostDTO(post)) 
                                   .ToList();
                case "CreateDesc":
                    return postList.OrderByDescending(s => s.CreateTime)
                                   .Select(post => MapToPostDTO(post)) 
                                   .ToList();
                case "LikeAsc":
                    return postList.OrderBy(s => s.Likes)
                                   .Select(post => MapToPostDTO(post)) 
                                   .ToList();
                case "LikeDesc":
                    return postList.OrderByDescending(s => s.Likes)
                                   .Select(post => MapToPostDTO(post)) 
                                   .ToList();
                default:
                    return postList.OrderBy(s => s.CreateTime)
                                   .Select(post => MapToPostDTO(post))
                                   .ToList();
            }
        }



        private PostDTO MapToPostDTO(PostModel post)
        {
            return new PostDTO
            {
             
                    Id = post.Id,
                    CreateTime = post.CreateTime,
                    Title = post.Title,
                    Description = post.Description,
                    ReadingTime = post.ReadingTime,
                    Image = post.Image,
                    AuthorId = post.AuthorId,
                    Author = post.Author,
                    CommunityId = (Guid)post.CommunityId,
                    CommunityName = post.CommunityName,
                    AddressId = post.AddressId,
                    Likes = post.Likes,
                    HasLike = post.HasLike,
                    CommentsCount = post.CommentsCount,

                    Tags = _dbContext.TagPosts
                        .Where(tagPost => tagPost.IdPost == post.Id)
                        .Join(
                            _dbContext.Tag,
                            tagPost => tagPost.IdTeg,
                            tag => tag.Id,
                            (tagPost, tag) => _mapper.Map<TagDTO>(tag)
                        )
                        .ToList()
              
         
            };
        }

        public async Task<IActionResult> PostPostList(PostWithTagsDTO postDTO, Guid Id, Guid guid)
        {
            var author = await _dbContext.Author.FirstOrDefaultAsync(x => x.id == guid);
            var community = await _dbContext.Community.FirstOrDefaultAsync(x => x.Id == Id);
            if (author != null)
            {
                var newPost = new PostModel
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.UtcNow,
                    Title = postDTO.Title,
                    Description = postDTO.Description,
                    ReadingTime = postDTO.ReadingTime,
                    Image = postDTO.Image,
                    AuthorId = guid,
                    Author = author.FullName,
                    CommunityId = Id,
                    CommunityName = community.Name,
                    Likes = 0,
                    HasLike = false,
                    CommentsCount = 0,
                };

                await _dbContext.Posts.AddAsync(newPost);
                await _dbContext.SaveChangesAsync();

                Guid newPostId = newPost.Id;

                foreach (var tag in postDTO.Tags)
                {
                    foreach (var tagId in tag.Id)
                    {
                        var newTagPost = new TagPost
                        {
                            id = Guid.NewGuid(),
                            IdTeg = tagId,
                            IdPost = newPostId
                        };

                        await _dbContext.TagPosts.AddAsync(newTagPost);
                        await _dbContext.SaveChangesAsync();
                    }
                }
                return new OkObjectResult("Post created successfully.");
            }
            else
            {
                return new BadRequestObjectResult("Author or community not found.");
            }


        }

        public async Task<bool> SubscribeOnCommunity(Guid idCommunity, Guid idUser)
        {
            var existingSubscription = await _dbContext.CommunityUserModels
                .FirstOrDefaultAsync(x => x.UserId == idUser && x.CommunityId == idCommunity);

            if (existingSubscription == null)
            {
                var newSubscription = new CommunityUserModel
                {
                    Id = Guid.NewGuid(),
                    CommunityId = idCommunity,
                    UserId = idUser,
                    Role = "Участник"
                };

                await _dbContext.CommunityUserModels.AddAsync(newSubscription);
                await _dbContext.SaveChangesAsync();

                return true; 
            }

            return false; 
        }




        public async Task UnsubscribeFromCommunity(Guid idCummunity, Guid idUser)
        {
            var subscription = await _dbContext.CommunityUserModels.FirstOrDefaultAsync(x => x.UserId == idUser && x.CommunityId == idCummunity);

            if (subscription != null)
            {
                _dbContext.CommunityUserModels.Remove(subscription);
                await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("User is not subscribed to this community.");
            }
        }



        public async Task<string> CommunityRole(Guid idCummunity, Guid idUser)
        {
            var cheackRole = await _dbContext.CommunityUserModels.FirstOrDefaultAsync(x => x.UserId == idUser);
            if (cheackRole != null)
            {
                var userRole = cheackRole.Role;
                return $"Ваша роль: {userRole}";
            }
            else
            {
                return "Пользователь не найден";
            }
        }

    }

}

