using PagedList;
using Project_Real__estate.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Project_Real__estate.Controllers
{
    public class AdvertisementsController : BaseController
    {
        private projectEntities1 db = new projectEntities1();

        // GET: Advertisements
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageSize, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.RDateSortParm = sortOrder == "RDate" ? "rdate_desc" : "RDate";
            ViewBag.EDateSortParm = sortOrder == "EDate" ? "edate_desc" : "EDate";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            //

            //var advertisements = from s in db.advertisements join
            //             g in db.Genres on
            //             s.GenreId equals g.GenreId
            //               select s;
            var advertisements = db.Advertisements.Include(a => a.Agent).Include(a => a.Category).Include(a => a.Payment).Include(a => a.Seller).Include(a => a.User);
            if (!String.IsNullOrEmpty(searchString))
            {
                advertisements = advertisements.Where(s => s.Tiltle.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    advertisements = advertisements.OrderByDescending(s => s.Tiltle);
                    break;
                case "price":
                    advertisements = advertisements.OrderBy(s => s.priceOfAds);
                    break;
                case "price_desc":
                    advertisements = advertisements.OrderByDescending(s => s.priceOfAds);
                    break;
                case "RDate":
                    advertisements = advertisements.OrderBy(s => s.ReleaseDate);
                    break;
                case "rdate_desc":
                    advertisements = advertisements.OrderByDescending(s => s.ReleaseDate);
                    break;
                case "EDate":
                    advertisements = advertisements.OrderBy(s => s.ExpirationDate);
                    break;
                case "edate_desc":
                    advertisements = advertisements.OrderByDescending(s => s.ExpirationDate);
                    break;
                default:  // title ascending 
                    advertisements = advertisements.OrderBy(s => s.Tiltle);
                    break;
            }
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaSize = (pageSize ?? 5);
            ViewBag.psize = defaSize;
            int i = 0;
            i++;
            ViewBag.pageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
             };


            return View(advertisements.ToPagedList(pageIndex, defaSize));
        }
        public ActionResult NotActivate(string sortOrder, string currentFilter, string searchString, int? pageSize, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.RDateSortParm = sortOrder == "RDate" ? "rdate_desc" : "RDate";
            ViewBag.EDateSortParm = sortOrder == "EDate" ? "edate_desc" : "EDate";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            //

            //var advertisements = from s in db.advertisements join
            //             g in db.Genres on
            //             s.GenreId equals g.GenreId
            //               select s;
            var advertisements = db.Advertisements.Include(a => a.Agent).Include(a => a.Category).Include(a => a.Payment).Include(a => a.Seller).Include(a => a.User).Where(a => a.isActivate == false);
            if (!String.IsNullOrEmpty(searchString))
            {
                advertisements = advertisements.Where(s => s.Tiltle.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    advertisements = advertisements.OrderByDescending(s => s.Tiltle);
                    break;
                case "price":
                    advertisements = advertisements.OrderBy(s => s.priceOfAds);
                    break;
                case "price_desc":
                    advertisements = advertisements.OrderByDescending(s => s.priceOfAds);
                    break;
                case "RDate":
                    advertisements = advertisements.OrderBy(s => s.ReleaseDate);
                    break;
                case "rdate_desc":
                    advertisements = advertisements.OrderByDescending(s => s.ReleaseDate);
                    break;
                case "EDate":
                    advertisements = advertisements.OrderBy(s => s.ExpirationDate);
                    break;
                case "edate_desc":
                    advertisements = advertisements.OrderByDescending(s => s.ExpirationDate);
                    break;
                default:  // title ascending 
                    advertisements = advertisements.OrderBy(s => s.Tiltle);
                    break;
            }
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaSize = (pageSize ?? 5);
            ViewBag.psize = defaSize;
            int i = 0;
            i++;
            ViewBag.pageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
             };


            return View(advertisements.ToPagedList(pageIndex, defaSize));
        }
        public ActionResult Activate(string sortOrder, string currentFilter, string searchString, int? pageSize, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "price" ? "price_desc" : "price";
            ViewBag.RDateSortParm = sortOrder == "RDate" ? "rdate_desc" : "RDate";
            ViewBag.EDateSortParm = sortOrder == "EDate" ? "edate_desc" : "EDate";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            //

            //var advertisements = from s in db.advertisements join
            //             g in db.Genres on
            //             s.GenreId equals g.GenreId
            //               select s;
            var advertisements = db.Advertisements.Include(a => a.Agent).Include(a => a.Category).Include(a => a.Payment).Include(a => a.Seller).Include(a => a.User).Where(a => a.isActivate == true);
            if (!String.IsNullOrEmpty(searchString))
            {
                advertisements = advertisements.Where(s => s.Tiltle.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title_desc":
                    advertisements = advertisements.OrderByDescending(s => s.Tiltle);
                    break;
                case "price":
                    advertisements = advertisements.OrderBy(s => s.priceOfAds);
                    break;
                case "price_desc":
                    advertisements = advertisements.OrderByDescending(s => s.priceOfAds);
                    break;
                case "RDate":
                    advertisements = advertisements.OrderBy(s => s.ReleaseDate);
                    break;
                case "rdate_desc":
                    advertisements = advertisements.OrderByDescending(s => s.ReleaseDate);
                    break;
                case "EDate":
                    advertisements = advertisements.OrderBy(s => s.ExpirationDate);
                    break;
                case "edate_desc":
                    advertisements = advertisements.OrderByDescending(s => s.ExpirationDate);
                    break;
                default:  // title ascending 
                    advertisements = advertisements.OrderBy(s => s.Tiltle);
                    break;
            }
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaSize = (pageSize ?? 5);
            ViewBag.psize = defaSize;
            int i = 0;
            i++;
            ViewBag.pageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
             };


            return View(advertisements.ToPagedList(pageIndex, defaSize));
        }

        // GET: Advertisements/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // GET: Advertisements/Create
        public ActionResult Create()
        {
            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgentName");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            ViewBag.PaymentId = new SelectList(db.Payments, "PaymentId", "PaymentName");
            ViewBag.SellerId = new SelectList(db.Sellers, "SellerId", "Name");
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "adsId,Tiltle,ReleaseDate,ExpirationDate,SellerId,AgentId,PaymentId,CategoryId,Describe,CurrentSymbol,priceOfAds,EstatePrice,Facade,Gateway,floors,Bedrooms,Toilets,furniture,Area,Cityprovince,District,Ward,Street,isActivate,UserId,StatusHouse")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                db.Advertisements.Add(advertisement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgentName", advertisement.AgentId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", advertisement.CategoryId);
            ViewBag.PaymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", advertisement.PaymentId);
            ViewBag.SellerId = new SelectList(db.Sellers, "SellerId", "Name", advertisement.SellerId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", advertisement.UserId);
            return View(advertisement);
        }

        // GET: Advertisements/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgentName", advertisement.AgentId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", advertisement.CategoryId);
            ViewBag.PaymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", advertisement.PaymentId);
            ViewBag.SellerId = new SelectList(db.Sellers, "SellerId", "Name", advertisement.SellerId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", advertisement.UserId);
            return View(advertisement);
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "adsId,Tiltle,SellerId,AgentId,PaymentId,CategoryId,Describe,CurrentSymbol,priceOfAds,EstatePrice,Facade,Gateway,floors,Bedrooms,Toilets,furniture,Area,Cityprovince,District,Ward,Street,isActivate,UserId,StatusHouse")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                var data = db.Advertisements.Find(advertisement.adsId);
                advertisement.ReleaseDate = data.ReleaseDate;
                advertisement.ExpirationDate = data.ExpirationDate;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(data).CurrentValues.SetValues(advertisement);                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgentName", advertisement.AgentId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", advertisement.CategoryId);
            ViewBag.PaymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", advertisement.PaymentId);
            ViewBag.SellerId = new SelectList(db.Sellers, "SellerId", "Name", advertisement.SellerId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", advertisement.UserId);
            return View(advertisement);
        }

        // GET: Advertisements/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement advertisement = db.Advertisements.Find(id);
            if (advertisement == null)
            {
                return HttpNotFound();
            }
            return View(advertisement);
        }

        // POST: Advertisements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Advertisement ads = db.Advertisements.FirstOrDefault(t => t.adsId == id);
            foreach (var item in ads.Reports.ToList())
            {               
                db.Reports.Remove(item);
            }
            foreach (var image in ads.Images.ToList())
            {
                db.Images.Remove(image);
            }
            db.Advertisements.Remove(ads);
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
