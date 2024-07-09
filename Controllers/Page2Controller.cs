using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using WebApplication1;

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
            .Take(16)           // 16 because we will delete first element
            .ToList();

        var userIds = userFoods
            .Select(uf => uf.UserId)
            .Distinct()
            .ToList();
        var users = _context.Users
            .Where(u => userIds.Contains(u.Id))
            .ToDictionary(u => u.Id, u => u);

        var foodIds = userFoods
            .Select(uf => uf.FoodId)
            .Distinct()
            .ToList();
        var foods = _context.Foods
            .Where(f => foodIds.Contains(f.Id))
            .ToDictionary(f => f.Id, f => f.Name);


        // User Info
        var user = users[userFoods.FirstOrDefault().UserId];
        var food = userFoods.FirstOrDefault();
        var foodName = foods[food.FoodId];
        var today_cntr = userFoods
            .Where(f => f.Date.Day == DateTime.Now.Day && 
                             f.Date.Month == DateTime.Now.Month && 
                             f.Date.Year == DateTime.Now.Year &&
                             f.FoodId == food.FoodId)
            .Count();
        var user_cntr = userFoods
            .Where(f => f.UserId == user.Id && f.FoodId == food.FoodId)
            .Count();

        userFoods = userFoods[1..];     // deleting first element

        string info = "";
        if (!Settings.registered)
        {
            info = $"{user.Name}, мы рады приветствовать вас в нашем сообществе! " +
                    $"Вы только что съели {foodName}! " +
                    $"За сегодня {foodName} было съедено {today_cntr} раз!";
        } 
        else
        {
            info = $"{user.Name}, с возвращением! " +
                   $"Вы только что съели {foodName}! " +
                   $"Всего вы съели {foodName} {user_cntr} раз! " +
                   $"За сегодня {foodName} было съедено {today_cntr} раз!";
        }


        // Users List
        List<string> usersDescriptions = new List<string>();
        foreach (var userFood in userFoods)
        {
            var us = users[userFood.UserId];
            string date = $"{userFood.Date.Hour}:{userFood.Date.Minute} {userFood.Date.Day}/{userFood.Date.Month}/{userFood.Date.Year}";
            string temp = $"{date} {us.Name} ({us.Email}) съел {foods[userFood.FoodId]}";

            usersDescriptions.Add(temp);
        }


        return View(new Page2ViewModel
        {
            Users = usersDescriptions,
            UserInfo = info
        });
    }
}
