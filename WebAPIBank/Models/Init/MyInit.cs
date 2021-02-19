using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebAPIBank.Models.Context;
using WebAPIBank.Models.Entities;

namespace WebAPIBank.Models.Init
{
    //CreateDateBaseIfNotExists sınıfı eger Database'iniz yoksa DB'niz ilk kez calıstıgında tetiklenen bir sınıftır. Yani eger Database'iniz varsa bu sınıf tetiklenmeyecektir. DropCreateDatabaseAlways  ve DropCreateDatabaseIfModelChanges sınıfları ise sırasıyla veritabanını sürekli sil ve yarat, veritabanını sadece model bir degişiklige ugradıgında sil ve yarat görevlerini yaparlar...

    //Her halükarda bu üc sınıfın da virtual olarak tanımlanmıs bir seed metodu vardır...
    public class MyInit:CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            CardInfo cif = new CardInfo();
            cif.CardUserName = "Yavuz Derbazlar";
            cif.CardNumber = "1111 1111 1111 1111";
            cif.CardExpiryYear = 2022;
            cif.CardExpiryMonth = 12;
            cif.SecurityNumber = "222";
            cif.Limit = 30000;
            cif.Balance = 30000;
            context.Cards.Add(cif);
            context.SaveChanges();
        }
    }
}