using AutoMapper;
using Blog.Data;
using Blog.Data.DTO;
using Blog.Data.Models;
using Blog.Services.Interfaces;
using Blog2023.Data.DTO;
using Blog2023.Data.enums;
using Blog2023.Data.Models;
using Blog2023.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;



namespace Blog.Services
{

    public class PostService : IPostService
    {
        private readonly ApplicationDbContext _dbContext;


        public PostService(ApplicationDbContext context, IMapper mapper)
        {
            _dbContext = context;
        }

        public async Task<PostWithPagination> GetPost(Guid[]? tags, string author, int? min, int? max, PostSorting? sorting, int page, int size, Guid userId)
        {
            IQueryable<PostModel> query = _dbContext.Posts.Include(one => one.Tags);

            var userCommunityIds = await _dbContext.CommunityUserModels
                .Where(uc => uc.UserId == userId)
                .Select(uc => uc.CommunityId)
                .ToListAsync();

            var closedCommunityIds = await _dbContext.Community
                .Where(c => c.IsClosed)
                .Select(c => c.Id)
                .ToListAsync();

            query = query.Where(post =>
                userCommunityIds.Contains(post.CommunityId) &&
                (!closedCommunityIds.Any() || !closedCommunityIds.Contains(post.CommunityId)));


            if (tags != null && tags.Any())
            {
                var postIdsForTags = await _dbContext.TagPosts
                    .Where(tp => tags.Contains(tp.IdTeg))
                    .Select(tp => tp.IdPost)
                    .ToListAsync();

                query = query.Where(post => postIdsForTags.Contains(post.Id));
            }

            if (min != null)
            {
                query = query.Where(post => post.ReadingTime >= min);
            }

            if (max != null)
            {
                query = query.Where(post => post.ReadingTime <= max);
            }

            if (!string.IsNullOrEmpty(author))
            {
                query = query.Where(a => a.Author.Contains(author));
            }

           

        int totalPostsCount = await query.CountAsync();
            int pageCount = (int)Math.Ceiling(totalPostsCount / (decimal)size);

            if (page > pageCount && pageCount != 0)
            {
                return null;
            }

            var posts = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();

            var postIds = posts.Select(post => post.Id).ToList();

            var tagIdsPerPost = await _dbContext.TagPosts
                .Where(tp => postIds.Contains(tp.IdPost))
                .Select(tp => new { tp.IdPost, tp.IdTeg })
                .ToListAsync();

            var tagIds = tagIdsPerPost.Select(tp => tp.IdTeg).Distinct().ToList();

            var TagsName = await _dbContext.Tag
                .Where(tag => tagIds.Contains(tag.Id))
                .ToListAsync();

            var tagMap = tagIdsPerPost.GroupBy(tp => tp.IdPost)
                .ToDictionary(group => group.Key, group => group.Select(tp => tp.IdTeg).ToList());

            var PostList = new PostWithPagination
            {
                Posts = posts.Select(post => new PostDTO
                {
                    Id = post.Id,
                    CreateTime = post.CreateTime,
                    Title = post.Title,
                    Description = post.Description,
                    ReadingTime = post.ReadingTime,
                    Image = post.Image,
                    AuthorId = post.AuthorId,
                    CommunityId = post.CommunityId,
                    CommunityName = post.CommunityName,
                    AddressId = post.AddressId,
                    Author = post.Author,
                    Likes = post.Likes,
                    HasLike = HasLike(userId, post.Id),
                    CommentsCount = post.CommentsCount,
                    Tags = tagMap.ContainsKey(post.Id)
                        ? TagsName.Where(tag => tagMap[post.Id].Contains(tag.Id))
                            .Select(tag => new TagDTO
                            {
                                Id = tag.Id,
                                Name = tag.Name,
                                CreateTime = tag.CreateTime
                            }).ToList()
                        : new List<TagDTO>(),
                    Comments = post.Comments
                }).ToList(),
                PaginationInfo = new PageInfoModel
                {
                    Size = size,
                    Count = pageCount,
                    Current = page
                }
            };

            return PostList;
        }




        public async Task<IActionResult> PostList(PostWithTagsDTO postDTO, Guid guid)
        {
            var author = await _dbContext.Author.FirstOrDefaultAsync(x => x.id == guid);
            if (author != null)
            {
                var postCreationResult = await CreatePost(postDTO, guid);
                return postCreationResult is OkObjectResult
                    ? (IActionResult)postCreationResult
                    : new OkObjectResult("Post and author created successfully.");
            }
            else
            {
                var newAuthorResult = await CreateAuthor(guid);
                if (newAuthorResult is OkObjectResult)
                {

                    var createdAuthor = await _dbContext.Author.FirstOrDefaultAsync(x => x.id == guid);

                    return await CreatePost(postDTO, createdAuthor.id);
                }
                return new OkObjectResult("Author or community not found.");
            }
        }



        public async Task<IActionResult> CreatePost(PostWithTagsDTO postDTO, Guid guid)
        {
            var author = await _dbContext.Author.FirstOrDefaultAsync(x => x.id == guid);

            if (string.IsNullOrWhiteSpace(postDTO.Title) || string.IsNullOrWhiteSpace(postDTO.Description) || postDTO.ReadingTime <= 0)
            {
                return new BadRequestObjectResult("Title, Description, or ReadingTime is empty or invalid.");
            }

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
                    Likes = 0,
                    HasLike = false,
                    CommentsCount = 0,
                };
                var postsCount = await _dbContext.Posts.CountAsync(p => p.AuthorId == guid);
                author.Posts = postsCount;
                await _dbContext.Posts.AddAsync(newPost);
                await _dbContext.SaveChangesAsync();

                _dbContext.Author.Update(author);
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


        public async Task<IActionResult> CreateAuthor(Guid guid)
        {
            var author = await _dbContext.Author.FirstOrDefaultAsync(x => x.id == guid);

            if (author == null)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == guid);

                if (user != null)
                {
                    var newAuthor = new AuthorModel
                    {
                        id = guid,
                        FullName = user.FullName,
                        BirthDate = user.BirthDate,
                        Gender = user.Gender,
                        Posts = 0,
                        Likes = 0,
                        Created = DateTime.UtcNow
                    };

                    _dbContext.Author.Add(newAuthor);
                    await _dbContext.SaveChangesAsync();
                    Console.WriteLine("Добавил нового автора");
                    return new OkObjectResult("Author created successfully.");
                }
                else
                {
                    return new BadRequestObjectResult("User not found.");
                }
            }
            else
            {
                return new BadRequestObjectResult("Author or community already exists.");
            }
        }

        public async Task<PostDTO> GetInfoPost(Guid id, Guid userId)
        {
            var post = await _dbContext.Posts
                .Include(t => t.Tags)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
            {
                return null;
            }

            var comments = _dbContext.CommentModels
     .Where(c => c.PostId == id)
     .OrderBy(c => c.CreatedTime)
     .ToList();

            var commentDTOs = comments.Select(comment => new CommentDTO
            {
                Id = comment.Id,
                CreatedDate = comment.CreatedTime,
                content = comment.Content,
                modifiedDate = comment.ModifiedDate,
                deleteDate = comment.DeleteDate,
                authorId = comment.AuthorId,
                author = comment.Author,
                subComments = comment.SubComments
            }).ToList();



            var tagIds = _dbContext.TagPosts
                .Where(x => x.IdPost == id)
                .Select(x => x.IdTeg)
                .ToList();

            var TagsName = await _dbContext.Tag
                .Where(x => tagIds.Contains(x.Id))
                .ToListAsync();

            var tagDTOs = TagsName.Select(tag => new TagDTO
            {
                Id = tag.Id,
                Name = tag.Name,
                CreateTime = tag.CreateTime
            }).ToList();

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
                Likes = post.Likes,
                HasLike = HasLike(userId, post.Id),
                CommentsCount = post.CommentsCount,
                Tags = tagDTOs,
                Comments = commentDTOs,
            };
        }



        public bool HasLike(Guid user, Guid post)
        {
            var usersLikedPost = _dbContext.UserLikes.Where(x => x.UserId == user && x.PostId == post).FirstOrDefault();
            if (usersLikedPost != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> LikeAdd(Guid IdPost, Guid idUser)
        {
            var userLikedPost = await _dbContext.UserLikes
                .FirstOrDefaultAsync(x => x.UserId == idUser && x.PostId == IdPost);

            if (userLikedPost == null)
            {
                var newLike = new UserLike
                {
                    Id = Guid.NewGuid(),
                    PostId = IdPost,
                    UserId = idUser
                };

                _dbContext.UserLikes.Add(newLike);

                var PlusLike = await _dbContext.Posts
                    .FirstOrDefaultAsync(post => post.Id == IdPost);

                if (PlusLike != null)
                {
                    PlusLike.Likes++;
                    await _dbContext.SaveChangesAsync();
                }

                await _dbContext.SaveChangesAsync();
                return true; 
            }
            else
            {
                Console.WriteLine("Лайк уже стоит");
                return false; 
            }
        }


        public async Task<bool> DeleteLike(Guid IdPost, Guid IdUser)
        {
            var userLike = await _dbContext.UserLikes
                .FirstOrDefaultAsync(like => like.UserId == IdUser && like.PostId == IdPost);

            if (userLike != null)
            {
                var postToUpdate = await _dbContext.Posts.FirstOrDefaultAsync(post => post.Id == IdPost);
                if (postToUpdate != null)
                {
                    postToUpdate.Likes--;
                    await _dbContext.SaveChangesAsync();
                }

                _dbContext.UserLikes.Remove(userLike);
                await _dbContext.SaveChangesAsync();
                return true; 
            }
            else
            {
                return false; 
            }
        }


    }
}









