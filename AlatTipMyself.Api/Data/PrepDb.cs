using AlatTipMyself.Api.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AlatTipMyself.Api.Data
{
    public static class PrepDb
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<TipMySelfContext>());
            }
        }

        private static void SeedData(TipMySelfContext context)
        {
            if (!context.UserDetails.Any())
            {
                Console.WriteLine("---> Seeding UserDetails Data...");
                context.UserDetails.AddRange(GetStaticUserDetails());
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("---> We already have data");
            }
        }

        public static IEnumerable<UserDetail> GetStaticUserDetails()
        {
            return new List<UserDetail>()
        {
            new UserDetail(){AcctNumber ="1002034567", FirstName = "Hassan", LastName="Daranijo", AcctBalance = 20000, Email="daranijohassan@gmail.com"},
            new UserDetail(){AcctNumber ="2056786789", FirstName = "Akinro", LastName ="Nelson", AcctBalance = 10000, Email="nelsonakinro@gmail.com"},
            new UserDetail(){AcctNumber ="4002035567", FirstName = "Horace", LastName="Akpan", AcctBalance = 50000, Email="horacewillie7@gmail.com"},
            new UserDetail(){AcctNumber ="0602036767", FirstName = "Peace", LastName="Ozon", AcctBalance = 26500, Email="PeaceOzon@gmail.com"}
        };
        }
    }

}
