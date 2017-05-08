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

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HartonoBankSystem.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ApplicationContext _context;
        public TransactionController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Deposit()
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deposit(ViewModels.Deposit depositViewModel)
        {
            try
            {
                var accountName = HttpContext.Session.GetString(SessionKeyAccountName);
                var accountId = Convert.ToInt32(HttpContext.Session.GetString(SessionKeyAccountId));

                if (ModelState.IsValid)
                {
                    Transaction transaction = new Transaction();
                    transaction.AccountID = accountId;
                    transaction.Amount = depositViewModel.Amount;
                    transaction.TransactionType = Transaction.TransactionTypeEnum.Deposit;

                    _context.Add(transaction);
                    var commitResult = await _context.SaveChangesAsync();
                    if (commitResult > 0)
                    {
                        var account = await _context.Accounts
                                            .AsNoTracking()
                                            .SingleOrDefaultAsync(x => x.AccountID == transaction.AccountID);
                        account.Balance += transaction.Amount;
                        _context.Update(account);
                        await _context.SaveChangesAsync();
                    }
                    return RedirectToAction("Index", "Home");
                }
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

        public async Task<IActionResult> Withdrawal()
        {
            var accountId = Convert.ToInt32(HttpContext.Session.GetString(SessionKeyAccountId));
            var account = await _context.Accounts
                                            .AsNoTracking()
                                            .SingleOrDefaultAsync(x => x.AccountID == accountId);
            ViewModels.Withdrawal withdrawalViewModel = new ViewModels.Withdrawal();
            withdrawalViewModel.AccountNumber = account.AccountNumber;
            withdrawalViewModel.Balance = account.Balance;
            return View(withdrawalViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Withdrawal(ViewModels.Withdrawal withdrawalViewModel)
        {
            try
            {
                var accountName = HttpContext.Session.GetString(SessionKeyAccountName);
                var accountId = Convert.ToInt32(HttpContext.Session.GetString(SessionKeyAccountId));

                if (ModelState.IsValid)
                {
                    Transaction transaction = new Transaction();
                    transaction.AccountID = accountId;
                    transaction.Amount = withdrawalViewModel.Amount;
                    transaction.TransactionType = Transaction.TransactionTypeEnum.Withdrawal;

                    _context.Add(transaction);
                    var commitResult = _context.SaveChanges();
                    if (commitResult > 0)
                    {
                        var account = _context.Accounts
                                            .AsNoTracking()
                                            .SingleOrDefault(x => x.AccountID == transaction.AccountID);
                        account.Balance -= transaction.Amount;
                        _context.Update(account);
                        _context.SaveChanges();
                    }
                    return RedirectToAction("Index", "Home");
                }
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


        public async Task<IActionResult> Transfer()
        {
            var accountId = Convert.ToInt32(HttpContext.Session.GetString(SessionKeyAccountId));
            var accountList = _context.Accounts
                                            .AsNoTracking()
                                            .AsQueryable();
            var ownerAccount = await accountList.SingleOrDefaultAsync(x => x.AccountID == accountId);
            ViewModels.Transfer transferViewModel = new ViewModels.Transfer();
            transferViewModel.AccountNumberFrom = ownerAccount.AccountNumber;
            transferViewModel.Balance = ownerAccount.Balance;

            ViewBag.TargetAccountList = await accountList.Where(x => x.AccountID != accountId).ToListAsync();

            return View(transferViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Transfer(ViewModels.Transfer transferViewModel)
        {
            try
            {
                var accountName = HttpContext.Session.GetString(SessionKeyAccountName);
                var accountId = Convert.ToInt32(HttpContext.Session.GetString(SessionKeyAccountId));

                if (ModelState.IsValid)
                {
                    Transaction transaction = new Transaction();
                    transaction.AccountID = accountId;
                    transaction.Amount = transferViewModel.Amount;
                    transaction.TargetAccountID = transferViewModel.TargetAccountId;
                    transaction.TransactionType = Transaction.TransactionTypeEnum.Transfer;

                    _context.Add(transaction);
                    var commitResult = _context.SaveChanges();
                    if (commitResult > 0)
                    {
                        var sourceAccount = _context.Accounts
                                            .AsNoTracking()
                                            .SingleOrDefault(x => x.AccountID == transaction.AccountID);
                        sourceAccount.Balance -= transaction.Amount;
                        _context.Update(sourceAccount);
                        _context.SaveChanges();

                        var targetAccount = _context.Accounts
                                            .AsNoTracking()
                                            .SingleOrDefault(x => x.AccountID == transaction.TargetAccountID);
                        targetAccount.Balance += transaction.Amount;
                        _context.Update(targetAccount);
                        _context.SaveChanges();
                    }
                    return RedirectToAction("Index", "Home");
                }
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
    }
}
