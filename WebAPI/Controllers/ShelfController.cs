using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace WebAPI.Controllers
{
    [Route("api/shelves")]
    [ApiController]
    public class ShelfController : ControllerBase
    {
        private readonly ShelfRepository repository;

        public ShelfController(ShelfRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Domain.Shelf>))]
        public IActionResult Index()
        {
            var result = this.repository.Filter().ToList();

            return this.Ok(result);
        }
    }
}
