using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FoodController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("api/food/add")]
        public IActionResult AddFood([FromBody] Food food)
        {
            if (_context.Foods.Any(f => f.Name == food.Name))
            {
                return BadRequest("Это блюдо уже кто-то когда-то ел");
            }

            _context.Foods.Add(food);
            _context.SaveChanges();

            return Ok(food);
        }
    }
}
