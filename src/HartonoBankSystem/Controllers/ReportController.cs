using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HartonoBankSystem.Data;
using HartonoBankSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using HartonoBankSystem.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace HartonoBankSystem.Controllers
{
    public class ReportController : BaseController
    {
        private readonly ApplicationContext _context;
        public ReportController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult ReportTransaction(ViewModels.ReportTransactionSearch reportTransactionModel)
        {
            var reportTransactionList = (from account in _context.Accounts
                                         join transaction in _context.Transactions
                                         on account.AccountID equals transaction.AccountID into temp
                                         from final in temp.DefaultIfEmpty()
                                         select new
                                         {
                                             TransactionType = (Transaction.TransactionTypeEnum)final.TransactionType,
                                             AccountName = account.AccountName,
                                             AccountBalance = account.Balance,
                                             CreatedDate = account.CreatedDate,
                                             TransactionAmount = final.Amount,
                                             AccountNumber = account.AccountNumber
                                         });

            return View(reportTransactionList);
        }
    }
}
