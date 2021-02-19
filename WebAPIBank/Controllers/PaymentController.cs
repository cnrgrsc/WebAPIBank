using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPIBank.DesignPatterns.SingletonPattern;
using WebAPIBank.Models.Context;
using WebAPIBank.Models.Entities;
using WebAPIBank.VMClasses;

namespace WebAPIBank.Controllers
{
    public class PaymentController : ApiController
    {
        MyContext _db;

        public PaymentController()
        {
            _db = DBTool.DBInstance;
        }

        //Alttaki Action test icindir!!!

        public List<PaymentVM> GetAll()
        {
            return _db.Cards.Select(x => new PaymentVM
            {
                CardUserName = x.CardUserName
            }).ToList();
        }

        //Post address
        //https://localhost:44363/api/Payment/ReceivePayment

        [HttpPost]

        public IHttpActionResult ReceivePayment(PaymentVM item)
        {
            CardInfo cif = _db.Cards.FirstOrDefault(x => x.CardNumber == item.CardNumber && x.SecurityNumber == item.SecurityNumber && x.CardUserName == item.CardUserName && x.CardExpiryYear == item.CardExpiryYear && item.CardExpiryMonth == item.CardExpiryMonth);

            if (cif != null)
            {
                if (cif.CardExpiryYear < DateTime.Now.Year)
                {
                    return BadRequest("Expired Card");
                }
                else if (cif.CardExpiryYear == DateTime.Now.Year)
                {
                    if (cif.CardExpiryMonth < DateTime.Now.Month)
                    {
                        return BadRequest("Expired Card");
                    }

                    if (cif.Balance>=item.ShoppingPrice)
                    {
                        return Ok();

                    }
                    else
                    {
                        //İLgili kartın balance'ini düsürürsünüz
                        return BadRequest("Balance exceeded");
                    }

                }

                if (cif.Balance >= item.ShoppingPrice)
                {
                    //İLgili kartın balance'ini düsürürsünüz
                    return Ok();
                }
                else
                {
                    return BadRequest("Balance exceeded");
                }
            }
            else
            {
                //Kart bulunamazsa

                return BadRequest("Card not found");
            }
        }
       
    }
}
