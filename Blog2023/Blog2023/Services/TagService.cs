using AutoMapper;
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

namespace Blog.Services
{

    public class TagService : ITagService
    {
        private readonly ApplicationDbContext _dbContext; 
        private readonly IMapper _mapper;

        public TagService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }       

        public async Task<List<TagDTO>> Tag()
        {
            var tagEntities = await _dbContext.Tag.ToListAsync();

            if (tagEntities != null && tagEntities.Any())
            {
                var tagDTOs = _mapper.Map<List<TagDTO>>(tagEntities);
                return tagDTOs;
            }

            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status401Unauthorized.ToString(), "No tags found");
            throw ex;
        }
    }


}

