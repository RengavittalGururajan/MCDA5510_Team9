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
    public class Train_DetailController : Controller
    {
       // public HttpSessionStateBase Session { get; }
        private Model2 db = new Model2();

        // GET: Train_Detail
        public ActionResult Index()
        {
            return View(db.Train_Detail.ToList());
        }

        // GET: Train_Detail/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Train_Detail train_Detail = db.Train_Detail.Find(id);
            if (train_Detail == null)
            {
                return HttpNotFound();
            }
            return View(train_Detail);
        }

        // GET: Train_Detail/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Search()
        {
            return View();
        }
        public ActionResult Book(int id)
        {
            Session["id"] = id;
           // return View(Url.Action("Book", "TrainReserveSystem.Controllers.Passenger_DetailsController", id ));
            return this.RedirectToAction("Book", "Passenger_Details",new { id=id});

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "source, destination,doj,passengercount")] Search searchattribute)
        {
            if(ModelState.IsValid)
            {
                string source = Request["source"].ToString();
                string destination = Request["destination"].ToString();
                string doj = Request["doj"].ToString();
                DateTime oDate = Convert.ToDateTime(doj);
                int passengercount = Convert.ToInt32(Request["passengercount"].ToString());
                if(passengercount>2)
                {
                    Session["searcherror"] = "Max 2 passengers per booking!";
                    return View("Search");
                }
                Train_For_Passenger trainsforpassenger = new Train_For_Passenger();
                var list = db.Train_Detail.Where( train => train.Source == source).Where(train=>train.Destination == destination).Where(train=>train.Date_Of_Travel.Equals(oDate)).Where(train=>train.Total_Seats_Available>passengercount);
                foreach(Train_Detail trains in list)
                {
                    string trainame = trains.Train_Name;
                    Console.WriteLine(" Train Name: " + trains.Train_Name);
                }
                List<Train_Detail> trainlist = list.ToList();
                if(trainlist.Count==0)
                {
                    Session["searcherror"] = "Sorry, no trains found!";
                    return View("Search");                                                                                                                                                                                                            
                }
                Session["source"] = source;
                Session["destination"] = destination;
                Session["doj"] = doj;
                Session["passengercount"]= passengercount;
                Session["trainlist"] = trainlist;
                Console.WriteLine(list);
            }
                
            return View("Train_detail_with_passenger_detail");
        }


        // POST: Train_Detail/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Train_Detail_ID,Train_Number,Source,Destination,Date_Of_Travel,Train_Name,Fare,Total_Seats_Available,Vacant_Seats")] Train_Detail train_Detail)
        {
            if (ModelState.IsValid)
            {
                db.Train_Detail.Add(train_Detail);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(train_Detail);
        }
        
        // GET: Train_Detail/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Train_Detail train_Detail = db.Train_Detail.Find(id);
            if (train_Detail == null)
            {
                return HttpNotFound();
            }
            return View(train_Detail);
        }

        // POST: Train_Detail/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Train_Detail_ID,Train_Number,Source,Destination,Date_Of_Travel,Train_Name,Fare,Total_Seats_Available,Vacant_Seats")] Train_Detail train_Detail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(train_Detail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(train_Detail);
        }

        // GET: Train_Detail/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Train_Detail train_Detail = db.Train_Detail.Find(id);
            if (train_Detail == null)
            {
                return HttpNotFound();
            }
            return View(train_Detail);
        }

        // POST: Train_Detail/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Train_Detail train_Detail = db.Train_Detail.Find(id);
            db.Train_Detail.Remove(train_Detail);
            db.SaveChanges();
            return RedirectToAction("Index");
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
