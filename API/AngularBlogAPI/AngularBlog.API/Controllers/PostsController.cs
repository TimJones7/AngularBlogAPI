using AngularBlog.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AngularBlog.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly DevBlogDbContext _context;

        public PostsController(DevBlogDbContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            var posts = await _context.Posts.ToListAsync();
            return Ok(posts);
        }



        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetPostByIdAsync(Guid id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post is not null)
            {
                return Ok(post);
            }
            return NotFound();
        }








    }
}
