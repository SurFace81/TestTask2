using Microsoft.AspNetCore.Mvc;
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

        var userFood = new UserFood
        {
            Name = model.Name,
            Email = model.Email,
            FoodId = model.DishId,
            Date = DateTime.Now
        };

        _context.UserFoods.Add(userFood);
        _context.SaveChanges();

        //return Ok();
        //return RedirectToAction("Index", "Page2");
        return Ok(new { redirectTo = Url.Action("Index", "Page2") });
    }
}
