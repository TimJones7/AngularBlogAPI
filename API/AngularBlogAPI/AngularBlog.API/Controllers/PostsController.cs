using AngularBlog.API.Data;
using AngularBlog.API.Models.DTO;
using AngularBlog.API.Models.Entities;
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
        [ActionName("GetPostByIdAsync")]
        public async Task<IActionResult> GetPostByIdAsync(Guid id)
        {
            var post = await _context.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post is not null)
            {
                return Ok(post);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> AddPostAsync(AddPostRequest newPost)
        {
            //  Convert DTO to entity
            var post = new Post()
            {
                Title = newPost.Title,
                Content = newPost.Content,
                Author = newPost.Author,
                FeaturedImageUrl = newPost.FeaturedImageUrl,
                PublishedDate = newPost.PublishedDate,
                UpdatedDate = newPost.UpdatedDate,
                Summary = newPost.Summary, 
                UrlHandle = newPost.UrlHandle,
                Visible = newPost.Visible
            };
            
            post.Id = Guid.NewGuid();
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPostByIdAsync), new { id = post.Id }, post);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdatePostAsync([FromRoute] Guid id, UpdatePostRequest updatedPost) 
        {
          
            var existingPost = await _context.Posts.FindAsync(id);
            if(existingPost is not null)
            {
                existingPost.Title = updatedPost.Title;
                existingPost.Content = updatedPost.Content;
                existingPost.Author = updatedPost.Author;
                existingPost.FeaturedImageUrl = updatedPost.FeaturedImageUrl;
                existingPost.PublishedDate = updatedPost.PublishedDate;
                existingPost.UpdatedDate = updatedPost.UpdatedDate;
                existingPost.Summary = updatedPost.Summary;
                existingPost.UrlHandle = updatedPost.UrlHandle;
                existingPost.Visible = updatedPost.Visible;
                
                await _context.SaveChangesAsync();
                return Ok(updatedPost);
            }
            return NotFound();
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletePostAsync(Guid id)
        {
            var existingPost = await _context.Posts.FindAsync(id);

            if(existingPost is not null)
            {
                _context.Posts.Remove(existingPost);
                await _context.SaveChangesAsync();
                return Ok(existingPost);
            }
            return NotFound();
        }
    }
}

