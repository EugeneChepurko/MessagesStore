using MessagesStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;

        public HomeController(UserManager<User> userManager, ILogger<HomeController> logger)
        {
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
            Message message = new Message();

            User _user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (_user != null && _user.Email == User.Identity.Name)
            {
                _user.LastMessage = user.LastMessage;

                message.DateTime = DateTime.Now;
                message.ApplicationUserId = _user.Id;
                message.UserName = _user.Email;
                message.MessageBody = _user.LastMessage;
                message.User = _user;

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
