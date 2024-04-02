using Blog.Data.Models;
using Blog2023.Data.DTO;
using Blog2023.Data.Models;
using Blog2023.Migrations;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Blog.Data
{
    public class ApplicationDbContextAddress : DbContext
    {
        public ApplicationDbContextAddress(DbContextOptions<ApplicationDbContextAddress> options) : base(options) { }

        public virtual DbSet<as_adm_hierarchy> as_adm_hierarchy { get; set; }
        public virtual DbSet<As_addr_obj> as_addr_obj { get; set; }
        public virtual DbSet<as_houses> as_houses { get; set; }

    }
}


