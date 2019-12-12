using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem
{
    public class BankSystemContext :DbContext
    {
        public BankSystemContext() : base("DbConnection")
        {
            
        }

        public DbSet<Account> Accounts { get; set; }
    }
}
