using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Repositories;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoRepository _service;
        public TodoController(ITodoRepository service)
        {
            _service = service;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
    } 
}
