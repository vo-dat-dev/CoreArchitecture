using Microsoft.AspNetCore.Mvc;

namespace CoreArchitecture.Controllers
{
    [Route("tree")]
    [ApiController]
    public class TreeController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetTree()
        {
            // This is a placeholder for the actual tree data retrieval logic.
            // In a real application, you would retrieve the tree structure from a database or other data source.
            var treeData = new
            {
                Id = 1,
                Name = "Root",
                Children = new[]
                {
                    new { Id = 2, Name = "Child 1", Children = Array.Empty<object>() },
                    new { Id = 3, Name = "Child 2", Children = Array.Empty<object>() }
                }
            };

            return Ok(treeData);
        }
    }
};