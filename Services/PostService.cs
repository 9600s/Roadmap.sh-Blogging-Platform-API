public interface IPostService
{
    BlogPost CreatePost(BlogPost post);
    IEnumerable<BlogPost> GetAllPosts();
    BlogPost GetPostById(int id);
    void DeletePost(int id);
    
    void UpdatePost(int id, BlogPost post);
    
    List<BlogPost> SearchTerm(string term);
}

public class PostService : IPostService
{
    private readonly List<BlogPost> _posts = new List<BlogPost>();
    private int _nextId = 1;

    public BlogPost CreatePost(BlogPost post) // POST
    {
        post.Id = _nextId++;
        post.CreatedAt = DateTime.Now;
        _posts.Add(post);
        return post;
    }
    
    public IEnumerable<BlogPost> GetAllPosts() // GET
    {
        return _posts;
    }
    
    public BlogPost GetPostById(int id) // GET
    {
        return _posts.FirstOrDefault(p => p.Id == id);
    }
    
    public void DeletePost(int id) // DELETE
    {
        var post = _posts.FirstOrDefault(p => p.Id == id);
        if (post != null)
        {
            _posts.Remove(post);
        }
    }
    
    public void UpdatePost(int id, BlogPost post) // PUT
    {
        var existingPost = _posts.FirstOrDefault(p => p.Id == id);
        if (existingPost != null)
        {
            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            existingPost.Category = post.Category;
            existingPost.Tags = post.Tags;
            existingPost.UpdatedAt = DateTime.Now;
        }
    }

    public List<BlogPost> SearchTerm(string term)
    {
        List<BlogPost> searchResults = new List<BlogPost>();
        foreach (var post in _posts)
        {
            if (post.Title.Contains(term))
            {
                searchResults.Add(post);
            }
        }
        return searchResults;
    }
    
}