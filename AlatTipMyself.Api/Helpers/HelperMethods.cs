using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlatTipMyself.Api.Helpers
{
    public static class HelperMethods
    {
        public static void CreatePinHash(string pin, out byte[] pinHash, out byte[] pinSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                pinSalt = hmac.Key;
                pinHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pin));
            }
        }

        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt) 
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public static Task<bool> VerifyPasswordHash(string Password, byte[] passwordHash, byte[] passwordSalt) => Task.Run(() =>
        {
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentNullException("Password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Password));
                for (int i = 0; i < computedPasswordHash.Length; i++)
                {
                    if (computedPasswordHash[i] != passwordHash[i]) return false;

                }
            }

            return true;
        });


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
