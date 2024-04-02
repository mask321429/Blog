using AutoMapper;
using Azure;
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
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using Blog2023.Migrations;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Blog.Services
{

    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CommentService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<GetCommentDTO>> GetTree(Guid guid)
        {
            var comment = await _dbContext.CommentModels.FirstOrDefaultAsync(x => x.Id == guid);

            if (comment == null)
            {
                return null;
            }
            
            var commentsEntities = _dbContext.CommentModels
                .Where(x => x.ParentId == guid)
                .OrderBy(c => c.CreatedTime)
                .ToList();

            var allComments = new List<CommentModel> { comment };
            // Прохожусь по всем дочерним комментариям и добавляю их в список
            foreach (var comm in commentsEntities)
            {
                if (comm.DeleteDate == null)
                {
                    allComments.Add(comm);
                    var subcomments = GetAllComments(comm.Id, new List<CommentModel>());
                    if (subcomments != null)
                    {
                        allComments.AddRange(subcomments);
                    }
                }
            }

            var commentList = new List<GetCommentDTO>();

            foreach (var com in allComments)
            {
                var subCommentsCount = _dbContext.CommentModels.Count(x => x.ParentId == com.Id && x.DeleteDate == null);

                commentList.Add(new GetCommentDTO
                {
                    Id = com.Id,
                    modifiedDate = com.ModifiedDate,
                    CreatedDate = com.CreatedTime,
                    deleteDate = com.DeleteDate,
                    author = com.Author,
                    authorId = com.AuthorId,
                    content = com.DeleteDate != null ? "комментарий удален" : com.Content,
                    subComments = subCommentsCount
                });
            }

            return commentList;
        }


 

        public List<CommentModel> GetAllComments(Guid parentID, List<CommentModel> subcomments)
        {
            if (parentID == null)
            {
                return subcomments;
            }
            // Сортирую по времени
            var newComments = _dbContext.CommentModels
                .Where(x => x.ParentId == parentID && x.DeleteDate == null)
                .OrderBy(c => c.CreatedTime);
            foreach (var com in newComments)
            {
                subcomments.Add(com);
                GetAllComments(com.Id, subcomments);
            }
            return subcomments;
        }




       

        public async Task<IActionResult> AddComment(CreateCommentDTO commentDTO, Guid postId, Guid userId)
        {
            CommentModel commentsEntity = new CommentModel();
            var postEntity = _dbContext.Posts.FirstOrDefault(x => x.Id == postId);
            if (postEntity == null)
            {
                Console.WriteLine("1");
                return new BadRequestObjectResult("Данный пост не найден.");
            }
            var userEntity = _dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (userEntity == null)
            {
                Console.WriteLine("2");
                return new BadRequestObjectResult("Данный пользователь не найден");
            }
            if (commentDTO.Content != null)
            {
                var newComment = new CommentModel
                {
                    Id = Guid.NewGuid(),
                    Content = commentDTO.Content,
                    ModifiedDate = null,
                    DeleteDate = null,
                    AuthorId = userId,
                    Author = userEntity.FullName,
                    SubComments = 0,
                    ParentId = commentDTO.ParentId,
                    PostId = postId,
                    CreatedTime = DateTime.UtcNow,
                };
                var parentComment = _dbContext.CommentModels.FirstOrDefault(c => c.Id == commentDTO.ParentId);
                Console.WriteLine("6");

                if (commentDTO.ParentId != null)
                {
                    parentComment.SubComments += 1;
                    _dbContext.CommentModels.Update(parentComment);
                }

                var postCommentsCount = _dbContext.CommentModels.Count(c => c.PostId == postId);


                postEntity.CommentsCount = postCommentsCount;
                _dbContext.Posts.Update(postEntity);
                await _dbContext.SaveChangesAsync();
                await _dbContext.SaveChangesAsync();
                await _dbContext.CommentModels.AddAsync(newComment);
                await _dbContext.SaveChangesAsync();




                return new OkObjectResult("Post created successfully.");
            }
            if (commentDTO.ParentId != null)
            {
                var parentComment = _dbContext.CommentModels.FirstOrDefault(c => c.Id == commentDTO.ParentId);

                if (parentComment == null || parentComment.DeleteDate != null)
                {
                    Console.WriteLine("4");
                    return new BadRequestObjectResult("Данного комментария не существует или он был удален.");
                }

                Console.WriteLine("5");
            }

            else Console.WriteLine("7");

            return new OkObjectResult("Post created successfully.");
        }


        public async Task<IActionResult> EditComment(EditContentDTO NewContent, Guid idComment, Guid idUser)
        {
            var GetComment = await _dbContext.CommentModels
                .Where(x => x.Id == idComment)
                .FirstOrDefaultAsync();
            if (GetComment == null)
            {

                Console.WriteLine("1");
                return new BadRequestObjectResult("Данного комментария не существует или он был удален.");
            }
            if (GetComment != null && GetComment.AuthorId == idUser)
            {


                GetComment.Content = NewContent.Content;
                GetComment.ModifiedDate = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();

                return new OkObjectResult("Комментарий был успешно изменен");
            }
            return new OkObjectResult("Комментарий был изменен.");
        }


        public async Task<IActionResult> DeleteComment(Guid idComment, Guid IdUser)
        {
            var GetComment = await _dbContext.CommentModels
                .Where(x => x.Id == idComment)
                .FirstOrDefaultAsync();
            if (GetComment == null)
            {

                Console.WriteLine("1");
                return new BadRequestObjectResult("Данного комментария не существует или он был удален.");
            }
            if(GetComment != null && GetComment.AuthorId == IdUser)
            {
                GetComment.DeleteDate = DateTime.UtcNow;
                await _dbContext.SaveChangesAsync();

                return new OkObjectResult("Комментарий был успешно удален");
            }

                return new OkObjectResult("Комментарий был изменен.");
        }

    }

}

