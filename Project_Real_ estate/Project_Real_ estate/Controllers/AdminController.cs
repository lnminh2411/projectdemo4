using PagedList;
using Project_Real__estate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace Project_Real__estate.Controllers
{
    public class AdminController : Controller
    {
        private projectEntities1 db = new projectEntities1();
        // GET: Admin
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageSize, int? page)
        {
            if (Session["UserId"] != null)
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.AgentSortParm = sortOrder == "Agent" ? "Agent_desc" : "Agent";
                ViewBag.SellerSortParm = sortOrder == "Seller" ? "Seller_desc" : "Seller";

                ViewBag.AdsCount = db.Advertisements.Count();
                ViewBag.AgentCount = db.Agents.Count();
                ViewBag.SellerCount = db.Sellers.Count();


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

                //var agents = from s in db.agents join
                //             g in db.Genres on
                //             s.GenreId equals g.GenreId
                //               select s;
                var agents = db.Reports.Include(a => a.Agent).Include(a => a.Advertisement).Include(a => a.Seller);
                if (!String.IsNullOrEmpty(searchString))
                {
                    agents = agents.Where(s => s.Advertisement.Tiltle.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        agents = agents.OrderByDescending(s => s.Advertisement.Tiltle);
                        break;
                    case "Seller":
                        agents = agents.OrderBy(s => s.Seller.Name);
                        break;
                    case "Seller_desc":
                        agents = agents.OrderByDescending(s => s.Seller.Name);
                        break;
                    case "Agent":
                        agents = agents.OrderBy(s => s.Agent.AgentName);
                        break;
                    case "Agent_desc":
                        agents = agents.OrderByDescending(s => s.Agent.AgentName);
                        break;
                    default:  // Name ascending 
                        agents = agents.OrderBy(s => s.Advertisement.Tiltle);
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
                return View(agents.ToPagedList(pageIndex, defaSize));
                
            }
            else
            {
                return RedirectToAction("Login", "Admin");
            }

        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(password);
                var data = db.Users.Where(s => s.UserName.Equals(username) && s.Password.Equals(f_password)).ToList();

                if (data.Count() > 0)
                {
                    Session["UserId"] = data.FirstOrDefault().UserId;
                    Session["UserName"] = data.FirstOrDefault().UserName;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.Message = "Wrong username or password";
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
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

    }
}