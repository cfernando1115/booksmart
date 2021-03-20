using BookSmart.Data;
using Microsoft.AspNetCore.Mvc;

namespace BookSmart.Controllers
{
    [Route("Account")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        /*[HttpGet]
        public async Task<ActionResult> Login()
        {

        }*/
    }
}
