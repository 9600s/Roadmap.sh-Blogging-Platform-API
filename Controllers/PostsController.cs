using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        
        private readonly IPostService _postService;
        
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            return Ok(_postService.GetAllPosts());
        }
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var post = _postService.GetPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }
        
        [HttpGet("search/{term}")]
        public async Task<ActionResult<string>> Get(string term)
        {
            var searchResults = _postService.SearchTerm(term);
            if (searchResults.Count == 0)
            {
                return NotFound();
            }
            return Ok(searchResults);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post([FromBody] BlogPost post)
        {
            _postService.CreatePost(post);

            return "Post created";
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<string>> Put(int id, [FromBody] BlogPost post)
        {
            _postService.UpdatePost(id, post);
            return "Post updated";
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<string>> Delete(int id)
        {
            _postService.DeletePost(id);
            return "Post deleted";
        }

    }
}