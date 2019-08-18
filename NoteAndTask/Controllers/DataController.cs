using Microsoft.AspNetCore.Mvc;

namespace NoteAndTask.Controllers
{
    public class DataController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}