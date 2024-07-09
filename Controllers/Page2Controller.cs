using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Linq;

public class Page2Controller : Controller
{
    private readonly AppDbContext _context;

    public Page2Controller(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var userFoods = _context.UserFoods
            .OrderByDescending(uf => uf.Date)
            .Take(15)
            .ToList();

        var foodIds = userFoods.Select(uf => uf.FoodId).Distinct().ToList();
        var foods = _context.Foods
            .Where(f => foodIds.Contains(f.Id))
            .ToDictionary(f => f.Id, f => f.Name);

        ViewBag.FoodNames = foods;

        return View(userFoods);
    }
}