using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HartonoBankSystem.ViewModels;
using HartonoBankSystem.Data;
using HartonoBankSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;


namespace HartonoBankSystem.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ApplicationContext _context;
        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var accountId = Convert.ToInt32(HttpContext.Session.GetString(SessionKeyAccountId));
                var account = await _context.Accounts
                                                .AsNoTracking()
                                                .SingleOrDefaultAsync(x => x.AccountID == accountId);
                ViewModels.Deposit depositViewModel = new ViewModels.Deposit();
                depositViewModel.AccountNumber = account.AccountNumber;
                depositViewModel.Balance = account.Balance;
                return View(depositViewModel);
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return null;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove(SessionKeyAccountName);
            HttpContext.Session.Remove(SessionKeyAccountId);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

    }
}
