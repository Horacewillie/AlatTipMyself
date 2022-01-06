using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Helpers
{
    public static class HelperMethods
    {
        public static string HashPassword(string Password)
        {
            return BCrypt.Net.BCrypt.HashPassword(Password);
        }

        public static string HashTransactionPin(string TransactionPin)
        {
            return BCrypt.Net.BCrypt.HashPassword(TransactionPin);
        }

        public static bool VerifyPassword(string modelPassword, string userPassword)
        {
            return BCrypt.Net.BCrypt.Verify(modelPassword, userPassword);
        }


        public static  Task<string> GenerateAccountNumber() => Task.Run(() =>
        {
            var randNum = new Random();
            StringBuilder db = new StringBuilder();
            for (int i = 0; i < 10; i++)
            {
                db.Append(randNum.Next(1, 9));
            }

            return db.ToString();
        });
    }
}
