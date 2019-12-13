using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TrainReserveSystem.Models;

namespace TrainReserveSystem.Controllers
{
    public class PaymentsController : Controller
    {
        private Model2 db = new Model2();

        // GET: Payments
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Booking);
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.FK_Booking_ID = new SelectList(db.Bookings, "Booking_ID", "Booking_ID");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Payment_ID,Status,FK_Booking_ID")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FK_Booking_ID = new SelectList(db.Bookings, "Booking_ID", "Booking_ID", payment.FK_Booking_ID);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.FK_Booking_ID = new SelectList(db.Bookings, "Booking_ID", "Booking_ID", payment.FK_Booking_ID);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Payment_ID,Status,FK_Booking_ID")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FK_Booking_ID = new SelectList(db.Bookings, "Booking_ID", "Booking_ID", payment.FK_Booking_ID);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult paymentpage()
        {

            return View("Payment_Page");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ValidationForPayment([Bind(Include = "cardtype,name,creditcardnumber,expirymonth,expiry_year")] Payment_Details paymentdetails)
        {
            Session["errormessage"] = null;
            if (ModelState.IsValid)
            {
                String cardtype = paymentdetails.cardtype.ToString();
                String name = paymentdetails.name;
                String number = paymentdetails.creditcardnumber.ToString();
                int expmonth = paymentdetails.expirymonth;
                int expyear = paymentdetails.expiry_year;

                if (cardtype.Equals("visa"))
                {
                    int length = number.Length;
                    if (length != 16)
                    {
                        Session["errormessage"] = "Visa Card Number should have 16 digits!";
                        return View("Payment_Page");
                    }
                    if (number[0] != '4')
                    {
                        Session["errormessage"] = "Visa Card Number should start by 4";
                        return View("Payment_Page");
                    }
                    if (expmonth < 1)
                    {
                        Session["errormessage"] = "Sorry, Expiry month is out of range!";
                        String error = Session["errormessage"].ToString();
                        return View("Payment_Page");
                    }
                    if (expmonth > 12)
                    {
                        Session["errormessage"] = "Sorry, Expiry month is out of range!";
                        String error = Session["errormessage"].ToString();
                        return View("Payment_Page");
                    }
                    if (expyear < 2016)
                    {
                        Session["errormessage"] = "Sorry, Expiry Year is out of range!";
                        return View("Payment_Page");
                    }
                    if (expyear > 2031)
                    {
                        Session["errormessage"] = "Sorry, Expiry Year is out of range!";
                        return View("Payment_Page");
                    }
                }
                if (cardtype.Equals("mastercard"))
                {
                    int length = number.Length;
                    if (length != 16)
                    {
                        Session["errormessage"] = "MasterCard Number should have 16 digits!";
                        return View("Payment_Page");
                    }
                    if (number[0]!='5')
                    {
                        Session["errormessage"] = "Master Card Number should start by 51-55!";
                        return View("Payment_Page");
                    }
                    char secchar = number[1];
                    int secondnumber = (int)Char.GetNumericValue(secchar);
                    if(secondnumber>6)
                    {
                        Session["errormessage"] = "Master Card Number should start by 51-55!";
                        return View("Payment_Page");
                    }
                    if (expmonth < 1)
                    {
                        Session["errormessage"] = "Sorry, Expiry month is out of range!";
                        return View("Payment_Page");
                    }
                    if (expmonth > 12)
                    {
                        Session["errormessage"] = "Sorry, Expiry month is out of range!";
                        return View("Payment_Page");
                    }
                    if (expyear < 2016)
                    {
                        Session["errormessage"] = "Sorry, Expiry year is out of range!";
                        return View("Payment_Page");
                    }
                    if (expyear > 2031)
                    {
                        Session["errormessage"] = "Sorry, Expiry year is out of range!";
                        return View("Payment_Page");
                    }
                }
                if (cardtype.Equals("americanexpress"))
                {
                    int length = number.Length;
                    if (length != 15)
                    {
                        Session["errormessage"] = "American Card Number should have 15 digits!";
                        return View("Payment_Page");
                    }
                    if (number[0]!='3')
                    {
                        Session["errormessage"] = "Amrican Card Number should start by 34-37!";
                        return View("Payment_Page");
                    }
                    char secchar = number[1];
                    int secondnumber = (int)Char.GetNumericValue(secchar);
                    if (secondnumber<4 || secondnumber>7)
                    {
                        Session["errormessage"] = "Amrican Card Number should start by 34-37!";
                        return View("Payment_Page");
                    }
                    if (expmonth < 1)
                    {
                        Session["errormessage"] = "Sorry, Expiry month is out of range!";
                        return View("Payment_Page");
                    }
                    if (expmonth > 12)
                    {
                        Session["errormessage"] = "Sorry, Expiry month is out of range!";
                        return View("Payment_Page");
                    }
                    if (expyear < 2016)
                    {
                        Session["errormessage"] = "Sorry, Expiry year is out of range!";
                        return View("Payment_Page");
                    }
                    if (expyear > 2031)
                    {
                        Session["errormessage"] = "Sorry, Expiry year is out of range!";
                        return View("Payment_Page");
                    }
                }
                
                Console.WriteLine(cardtype);
                Console.WriteLine(name);
                Console.WriteLine(number);
                if(Session["errormessage"]==null)
                {
                 //   List<Passenger_Details> passengerlist = (List<TrainReserveSystem.Models.Train_Detail>)Session["passengerlist"];
                    return View("PaymentConfirmation");
                }
            }

            return View("Payment_Page");


            return View("Payment_Details");

        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
