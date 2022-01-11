using AlatTipMyself.Api;
using AlatTipMyself.Api.Controllers;
using AlatTipMyself.Api.Data;
using AlatTipMyself.Api.Models;
using AlatTipMyself.Api.Services;
using AutoMapper;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AlatXtest
{
    public class TransactionsControllerTest
    {
        [Fact]           
        public async Task Test1Async()
            {                       
                 var data1 = A.Fake<ITransactionService>();
                 var data2 = A.Fake<IMapper>();
                 var data3 = A.Fake<IUserService>();
                 var data4 = A.Fake<TipMySelfContext>();                
            var sendMoneyParameter = new SendMoneyParameter
            {
                Amount = 5m,
                ToAccount = "1002034567"
            };
            string ToAcct = "1002034567";
            decimal amt = 50m;
            string FromAccount = "4002035567";
            var context = new TipMySelfContext();
            var sourceAccount = context.UserDetails.SingleOrDefault(p => (p.AcctBalance).ToString() == FromAccount);         
             //  var tested = new TransactionsController(data1,data2,data3);
            //  var fake = A.CollectionOfDummy<string,SendMoneyParameter>((int.Parse)(FromAccount)).AsEnumerable();
            // A.CallTo(() => data1.SendMoneyAsync(FromAccount,sendMoneyParameter.ToAccount, sendMoneyParameter.Amount)).Returns(Task.FromResult(fake));          
            var tested1 = new TransactionService(context);        
          //  UserDetail actionResult=  tested1.SendMoneyAsync(FromAccount, sourceAccount.AcctNumber, sendMoneyParameter.Amount)as UserDetail;
          //  var actionResult= await Task.Run(() => (tested1.SendMoneyAsync(FromAccount, sourceAccount.AcctNumber, sendMoneyParameter.Amount)));
            var actionResult = await Task.Run(() => (tested1.SendMoneyAsync(FromAccount, ToAcct, amt)));
           
        }


    }
}
