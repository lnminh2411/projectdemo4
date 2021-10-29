using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Project_Real__estate.Models;

namespace Project_Real__estate.Controllers
{
   
    public class SellersController : BaseController
    {
        private projectEntities1 db = new projectEntities1();

        // GET: Sellers
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageSize, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UserSortParm = sortOrder == "User" ? "User_desc" : "User";           


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

            //var sellers = from s in db.sellers join
            //             g in db.Genres on
            //             s.GenreId equals g.GenreId
            //               select s;
            var sellers = db.Sellers.Include(a => a.User);
            if (!String.IsNullOrEmpty(searchString))
            {
                sellers = sellers.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    sellers = sellers.OrderByDescending(s => s.Name);
                    break;
                
                case "User":
                    sellers = sellers.OrderBy(s => s.User.UserName);
                    break;
                case "User_desc":
                    sellers = sellers.OrderByDescending(s => s.User.UserName);
                    break;
                default:  // Name ascending 
                    sellers = sellers.OrderBy(s => s.Name);
                    break;
            }
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaSize = (pageSize ?? 5);
            ViewBag.psize = defaSize;
            int i = 0;
            i++;
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
             };


            return View(sellers.ToPagedList(pageIndex, defaSize));
        }
        public ActionResult Activate(string sortOrder, string currentFilter, string searchString, int? pageSize, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UserSortParm = sortOrder == "User" ? "User_desc" : "User";           


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

            //var sellers = from s in db.sellers join
            //             g in db.Genres on
            //             s.GenreId equals g.GenreId
            //               select s;
            var sellers = db.Sellers.Include(a => a.User).Where(a=>a.isActivate == true);
            if (!String.IsNullOrEmpty(searchString))
            {
                sellers = sellers.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    sellers = sellers.OrderByDescending(s => s.Name);
                    break;
                
                case "User":
                    sellers = sellers.OrderBy(s => s.User.UserName);
                    break;
                case "User_desc":
                    sellers = sellers.OrderByDescending(s => s.User.UserName);
                    break;
                default:  // Name ascending 
                    sellers = sellers.OrderBy(s => s.Name);
                    break;
            }
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaSize = (pageSize ?? 5);
            ViewBag.psize = defaSize;
            int i = 0;
            i++;
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
             };


            return View(sellers.ToPagedList(pageIndex, defaSize));
        }
        public ActionResult NotActivate(string sortOrder, string currentFilter, string searchString, int? pageSize, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UserSortParm = sortOrder == "User" ? "User_desc" : "User";


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

            //var sellers = from s in db.sellers join
            //             g in db.Genres on
            //             s.GenreId equals g.GenreId
            //               select s;
            var sellers = db.Sellers.Include(a => a.User).Where(a=>a.isActivate == false);
            if (!String.IsNullOrEmpty(searchString))
            {
                sellers = sellers.Where(s => s.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    sellers = sellers.OrderByDescending(s => s.Name);
                    break;

                case "User":
                    sellers = sellers.OrderBy(s => s.User.UserName);
                    break;
                case "User_desc":
                    sellers = sellers.OrderByDescending(s => s.User.UserName);
                    break;
                default:  // Name ascending 
                    sellers = sellers.OrderBy(s => s.Name);
                    break;
            }
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaSize = (pageSize ?? 5);
            ViewBag.psize = defaSize;
            int i = 0;
            i++;
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="5", Text= "5" },
                new SelectListItem() { Value="10", Text= "10" },
                new SelectListItem() { Value="15", Text= "15" },
                new SelectListItem() { Value="25", Text= "25" },
                new SelectListItem() { Value="50", Text= "50" },
             };


            return View(sellers.ToPagedList(pageIndex, defaSize));
        }
        // GET: Sellers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // GET: Sellers/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SellerId,Name,Gender,Email,Birthdate,Address,Phone,isActivate,Password,ConfirmPassword,UserId")] Seller seller)
        {
            if (ModelState.IsValid)
            {
                var check = db.Sellers.FirstOrDefault(s => s.Email == seller.Email);
                if (check == null)
                {
                    seller.Password = GetMD5(seller.Password);
                    seller.ConfirmPassword = seller.ConfirmPassword;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Sellers.Add(seller);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Sellers");
                }
                else
                {
                    ViewBag.error = "Email already existed";
                    return View();
                }
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", seller.UserId);
            return View(seller);
        }

        // GET: Sellers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", seller.UserId);
            return View(seller);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SellerId,Name,Gender,Email,Address,Phone,isActivate,Password,UserId")] Seller seller)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            if (ModelState.IsValid)
            {
                var data = db.Sellers.Find(seller.SellerId);
                seller.Password = data.Password;
                seller.ConfirmPassword = data.Password;
                seller.Birthdate = data.Birthdate;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(data).CurrentValues.SetValues(seller);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", seller.UserId);           
            return View(seller);
        }

        // GET: Sellers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return HttpNotFound();
            }
            return View(seller);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seller seller = db.Sellers.FirstOrDefault(t => t.SellerId == id);
            foreach (var item in seller.Advertisements.ToList())
            {
                foreach (var image in item.Images.ToList())
                {
                    db.Images.Remove(image);
                }
                db.Advertisements.Remove(item);
            }
            db.Sellers.Remove(seller); 
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




        public ActionResult Information()
        {
            if (Session["SellerId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Sellers");
            }
        }
        public ActionResult Register()
        {
            return View();
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Seller seller)
        {
            if (ModelState.IsValid)
            {
                var check = db.Sellers.FirstOrDefault(s => s.Email == seller.Email);
                if (check == null)
                {
                    seller.Password = GetMD5(seller.Password);
                    seller.ConfirmPassword = seller.ConfirmPassword;
                    seller.isActivate = false;
                    db.Sellers.Add(seller);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Sellers");
                }
                else
                {
                    ViewBag.error = "Email already existed";
                    return View();
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = db.Sellers.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).FirstOrDefault();
                if (data != null)
                {
                    if (data.isActivate == false)
                    {
                        ViewBag.Message = "Please wait for your account to be activated";
                        return View();
                    }
                    else
                    {
                        Session["SellerId"] = data.SellerId;
                        Session["Name"] = data.Name;
                        Session["Email"] = data.Email;
                        Session["Birthdate"] = data.Birthdate.ToString();
                        Session["Phone"] = data.Phone;
                        Session["Address"] = data.Address;
                        Session["Gender"] = Enum.GetName(typeof(Gender), data.Gender);
                        Session["isActivate"] = Enum.GetName(typeof(isActivate), data.isActivate);
                        return RedirectToAction("Information", "Sellers");
                    }
                }
                else
                {
                    ViewBag.Message = "Wrong email or password";
                }
            }

            return View();
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
    }
}



