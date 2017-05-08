using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HartonoBankSystem.Data;
using HartonoBankSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HartonoBankSystem.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ApplicationContext _context;
        public AccountController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> Index()
        {
            return View(await _context.Accounts.ToListAsync());
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Account accountViewModel)
        {
            if (accountViewModel == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                                .AsNoTracking()
                                .SingleOrDefaultAsync(x => x.AccountName == accountViewModel.AccountName && x.Password == accountViewModel.Password);

            if (account == null)
            {
                return NotFound();
            }
            else
            {
                HttpContext.Session.SetString(SessionKeyAccountName, account.AccountName);
                HttpContext.Session.SetString(SessionKeyAccountId, account.AccountID.ToString());
                return RedirectToAction("Index", "Home");
            }
        }

       

    }
}
