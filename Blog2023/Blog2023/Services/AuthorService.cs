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
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _dbContext; 
        private readonly IMapper _mapper;

        public AuthorService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }       

        public async Task<List<AuthorDTO>> GetAuthor()
        {
            var authorEntities = await _dbContext.Author.ToListAsync();

            if (authorEntities != null && authorEntities.Any())
            {
                var autorDTOs = _mapper.Map<List<AuthorDTO>>(authorEntities);
                return autorDTOs;
            }

            var ex = new Exception();
            ex.Data.Add(StatusCodes.Status401Unauthorized.ToString(), "No tags found");
            throw ex;
        }
    }
}

