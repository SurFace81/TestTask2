using Microsoft.AspNetCore.Mvc;
using WebApplication1;
using WebApplication1.Models;

public class Page1Controller : Controller
{
    private readonly AppDbContext _context;

    public Page1Controller(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var foods = _context.Foods.ToList();
        ViewBag.FoodList = foods;
        return View();
    }

    [HttpPost("api/page1/tellfood")]
    public IActionResult TellFood([FromBody] FoodInputModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Settings.registered = true;
        var user = _context.Users.FirstOrDefault(u => u.Name == model.Name && u.Email == model.Email);

        if (user == null)
        {
            user = new User
            {
                Name = model.Name,
                Email = model.Email
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            Settings.registered = false;
        }

        var userFood = new UserFood
        {
            UserId = user.Id,
            FoodId = model.DishId,
            Date = DateTime.Now
        };

        _context.UserFoods.Add(userFood);
        _context.SaveChanges();

        return Ok(new { redirectTo = Url.Action("Index", "Page2") });
    }
}
