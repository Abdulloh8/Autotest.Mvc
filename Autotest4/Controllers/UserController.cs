using Autotest4.Models;
using Autotest4.Repositories;
using Autotest4.Services;
using Microsoft.AspNetCore.Mvc;

namespace Autotest4.Controllers;


public class UserController : Controller
{
    private readonly UsersService _usersService;

    public UserController(UsersService usersService)
    {
        _usersService = usersService;
    }   

    [HttpGet]
    public IActionResult SingUp()
    {
        return View();
    }
    [HttpPost]
    public IActionResult SingUp(SingUpUser user)
    {
        if (!ModelState.IsValid)
        {
            return View();
        }
        string name = user.UserName;
        bool key = _usersService.Key(name);

        if (!key)
        {
            ModelState.AddModelError("UserName", "this UserName Have");
            return View();
        }
        _usersService.Register(user, HttpContext);

        return RedirectToAction("index", "Home");

    }
    [HttpGet]
    public IActionResult SingIn()
    {
        return View();
    }
    [HttpPost]
    public IActionResult SingIn(SingINUser usere)
    {
        if (!ModelState.IsValid)
        {
            return View(usere);
        }

        var user = _usersService.LogIn(usere, HttpContext);

        if (!user)
        {
            ModelState.AddModelError("UserName", "Not is User or password is invalid");
            return View();
        }

        return RedirectToAction("Profile");
    }
    public IActionResult Profile()
    {
        if (HttpContext.Request.Cookies.ContainsKey("user_id"))
        {
            
            var user = _usersService.GetCurrentUser(HttpContext);

            if (user == null)
            {
                return RedirectToAction("SingIn");
            }
            var list = new List<Natija>();
            list = _usersService.GetNatija(HttpContext);
            ViewBag.l = list;
            return View(user);
        }
        return RedirectToAction("SingUp");
    }
    public IActionResult setting()
    {
        var til = "uz";
        if (HttpContext.Request.Cookies.ContainsKey("til"))
        {
            til = HttpContext.Request.Cookies["til"];
        }
        ViewBag.til = til;
            return View();
    }
    public IActionResult EditUserName()
    {
        return View();
    }
    public IActionResult EditPassword()
    {
        return View();
    }
    public IActionResult DeleteAcaunt()
    {
        
        _usersService.Delete(HttpContext);

        return RedirectToAction("SingIn", "User");
    }
    public IActionResult Exir()
    {
        HttpContext.Response.Cookies.Delete("user_id");

        return RedirectToAction("SingIn", "User");
    }
    public IActionResult Til(string til)
    {
        HttpContext.Response.Cookies.Append("til", til);
        return RedirectToAction("setting", "User");
    }
    public IActionResult ConvertUserName(string UserName)
    {
        _usersService.UpdateUserName(HttpContext, UserName);

        return View();
    }
    public IActionResult ConvertPassword(string password)
    {
        _usersService.UpdatePassword(HttpContext, password);

        return View();
    }
    public IActionResult Obv()
    {
        _usersService.Obv(HttpContext);
        return RedirectToAction("index", "Tisket");
    }
}
