using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BCrypt.Net.BCrypt;

namespace BankSystem.Services
{
    public class AuthManager
    {

        public Account SignUp(string fullName, string mobileNumber, string password)
        {
            using (var context = new BankSystemContext())
            {
                if (Authentication(mobileNumber, context)) return null;

                var account = new Account
                {
                    FullName = fullName,
                    MobileNumber = mobileNumber,
                    Password = HashPassword(password)
                };

                context.Accounts.Add(account);
                context.SaveChanges();
                return account;
            }
        }

        public Account SignIn(string mobileNumber, string password)
        {
            using (var context = new BankSystemContext())
            {
                var user = context.Accounts.SingleOrDefault(x => x.MobileNumber == mobileNumber);
                if (user is null || !Verify(password, user.Password))
                    return null;
                return user;
            }
        }

        private bool Authentication(string mobileNumber, BankSystemContext context)
        {
            if (context.Accounts.SingleOrDefault(x => x.MobileNumber == mobileNumber) is null)
            {
                return false;
            }
            return true;
        }
    }
}
