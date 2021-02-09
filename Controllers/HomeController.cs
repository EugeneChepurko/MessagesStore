using MessagesStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly Models.AppContext _db;
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;

        public HomeController(Models.AppContext db, UserManager<User> userManager, ILogger<HomeController> logger)
        {
            _db = db;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        [DisableCors]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Index(User user)
        {
            User _user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (_user != null && _user.Email == User.Identity.Name)
            {
                _user.LastMessage = user.LastMessage;

                Message message = new Message();
                message.Initialize(_user.Id, _user.LastMessage, User.Identity.Name);

                Message foundedMessage = _db.Messages.FirstOrDefault(u => u.UserId == _user.Id);

                if (ModelState.IsValid)
                {
                    _db.Messages.Add(message);
                    await _db.SaveChangesAsync();
                }

                await _userManager.UpdateAsync(_user);
            }

            return View(_userManager.Users.ToList());
        }

        [DisableCors]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
