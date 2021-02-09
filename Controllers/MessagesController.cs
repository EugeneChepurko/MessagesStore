using MessagesStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace MessagesStore.Controllers
{
    public class MessagesController : Controller
    {
        private readonly Models.AppContext _db;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public MessagesController(Models.AppContext db, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _db = db;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        [HttpGet]
        public IActionResult ViewMyMessages(string id, string sortOrder)
        {
            ViewBag.IdSortParam = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.DateSortParam = sortOrder == "CurrentDate" ? "date_desc" : "CurrentDate";

            var allUserMessages = _db.Messages.ToList().FindAll(i => i.UserId == id);
            var messages = from message in allUserMessages
                           select message;

            switch (sortOrder)
            {
                case "id_desc":
                    messages = messages.OrderByDescending(s => s.Id);
                    break;
                case "CurrentDate":
                    messages = messages.OrderBy(s => s.CurrentDate);
                    break;
                case "date_desc":
                    messages = messages.OrderByDescending(s => s.CurrentDate);
                    break;
                default:
                    messages = messages.OrderBy(s => s.Id);
                    break;
            }
            return View(messages.ToList());
        }

        [Authorize]
        [HttpGet]
        public IActionResult ViewAllMessages(string sortOrder)
        {
            ViewBag.IdSortParam = string.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.DateSortParam = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Message> messages = from message in _db.Messages
                                           select message;
            switch (sortOrder)
            {
                case "id_desc":
                    messages = messages.OrderByDescending(s => s.Id);
                    break;
                case "Date":
                    messages = messages.OrderBy(s => s.CurrentDate);
                    break;
                case "date_desc":
                    messages = messages.OrderByDescending(s => s.CurrentDate);
                    break;
                default:
                    messages = messages.OrderBy(s => s.Id);
                    break;
            }
            return View(messages.ToList());
        }


        [Authorize]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            Message message = _db.Messages.FirstOrDefault(m => m.Id == id);

            if (message != null)
            {
                _db.Messages.Remove(message);
                await _db.SaveChangesAsync();

                return Redirect($"~/Messages/ViewMyMessages/{message.UserId}");
            }

            return Redirect($"~/Home/Index");
        }

    }
}
