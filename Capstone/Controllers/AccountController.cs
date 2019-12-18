using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Capstone.Models;
using System.Threading.Tasks;
using Capstone.ViewModels;
using System.Security.Claims;
using System.Linq;

namespace Capstone.Controllers
{
  public class AccountController : Controller
  {
    private readonly CapstoneContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, CapstoneContext db)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _db = db;
    }

    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userLocations = _db.Locations.Where(entry => entry.User.Id == currentUser.Id);
      return View(userLocations);
    }

    [HttpPost]
    public async Task<ActionResult> LogOff()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index");
    }

    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Login(LoginViewModel model)
    {
        Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: true, lockoutOnFailure: false);
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        else
        {
            return View();
        }
    }
    public ActionResult Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Register (RegisterViewModel model)
    {
        var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };
        if(this.ModelState.IsValid)
        {
          IdentityResult result = await _userManager.CreateAsync(user, model.Password);
          if (result.Succeeded)
          {
              return RedirectToAction("Index");
          }
          else
          {
              return View();
          }
        }
        else{
          return View();
        }

    }
  }
}
