using AngularBlog.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngularBlog.API.Data
{
    public class DevBlogDbContext : DbContext
    {


        public DevBlogDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Post> Posts { get; set; }


    }
}
