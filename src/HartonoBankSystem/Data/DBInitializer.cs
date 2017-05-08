using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HartonoBankSystem.Models;

namespace HartonoBankSystem.Data
{
    public class DBInitializer
    {
        public static void Initialize(ApplicationContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Accounts.Any())
            {
                return;   // DB has been seeded
            }

            var accounts = new Account[]
            {
            new Account{AccountName="hartono001",Balance=0, AccountNumber="accno001", Password="pass.123", CreatedDate = DateTime.Now },
            new Account{AccountName="hartono002",Balance=0, AccountNumber="accno002", Password="pass.123", CreatedDate = DateTime.Now },
            new Account{AccountName="hartono003",Balance=0, AccountNumber="accno003", Password="pass.123", CreatedDate = DateTime.Now },
            };
            foreach (Account accountItem in accounts)
            {
                context.Accounts.Add(accountItem);
            }
            context.SaveChanges();

        }
    }
}
