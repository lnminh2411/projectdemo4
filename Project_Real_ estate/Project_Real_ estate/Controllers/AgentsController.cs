using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
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
   
    public class AgentsController : BaseController
    {
        private projectEntities1 db = new projectEntities1();

        // GET: Agents
        
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? pageSize, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UserSortParm = sortOrder == "User" ? "User_desc" : "User";
            ViewBag.PaymentSortParm = sortOrder == "Payment" ? "Payment_desc" : "Payment";


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
            var agents = db.Agents.Include(a => a.User).Include(a=>a.Payment);
            if (!String.IsNullOrEmpty(searchString))
            {
                agents = agents.Where(s => s.AgentName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    agents = agents.OrderByDescending(s => s.AgentName);
                    break;
                case "Payment":
                    agents = agents.OrderBy(s => s.Payment.PaymentName);
                    break;
                case "Payment_desc":
                    agents = agents.OrderByDescending(s => s.Payment.PaymentName);
                    break;
                case "User":
                    agents = agents.OrderBy(s => s.User.UserName);
                    break;
                case "User_desc":
                    agents = agents.OrderByDescending(s => s.User.UserName);
                    break;
                default:  // Name ascending 
                    agents = agents.OrderBy(s => s.AgentName);
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
        public ActionResult Activate(string sortOrder, string currentFilter, string searchString, int? pageSize, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UserSortParm = sortOrder == "User" ? "User_desc" : "User";
            ViewBag.PaymentSortParm = sortOrder == "Payment" ? "Payment_desc" : "Payment";


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
            var agents = db.Agents.Include(a => a.User).Include(a => a.Payment).Where(a=>a.isActivate == true);
            if (!String.IsNullOrEmpty(searchString))
            {
                agents = agents.Where(s => s.AgentName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    agents = agents.OrderByDescending(s => s.AgentName);
                    break;
                case "Payment":
                    agents = agents.OrderBy(s => s.Payment.PaymentName);
                    break;
                case "Payment_desc":
                    agents = agents.OrderByDescending(s => s.Payment.PaymentName);
                    break;
                case "User":
                    agents = agents.OrderBy(s => s.User.UserName);
                    break;
                case "User_desc":
                    agents = agents.OrderByDescending(s => s.User.UserName);
                    break;
                default:  // Name ascending 
                    agents = agents.OrderBy(s => s.AgentName);
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
        public ActionResult NotActivate(string sortOrder, string currentFilter, string searchString, int? pageSize, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.UserSortParm = sortOrder == "User" ? "User_desc" : "User";
            ViewBag.PaymentSortParm = sortOrder == "Payment" ? "Payment_desc" : "Payment";


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
            var agents = db.Agents.Include(a => a.User).Include(a => a.Payment).Where(a=>a.isActivate== false);
            if (!String.IsNullOrEmpty(searchString))
            {
                agents = agents.Where(s => s.AgentName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    agents = agents.OrderByDescending(s => s.AgentName);
                    break;
                case "Payment":
                    agents = agents.OrderBy(s => s.Payment.PaymentName);
                    break;
                case "Payment_desc":
                    agents = agents.OrderByDescending(s => s.Payment.PaymentName);
                    break;
                case "User":
                    agents = agents.OrderBy(s => s.User.UserName);
                    break;
                case "User_desc":
                    agents = agents.OrderByDescending(s => s.User.UserName);
                    break;
                default:  // Name ascending 
                    agents = agents.OrderBy(s => s.AgentName);
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

        // GET: Agents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // GET: Agents/Create
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            ViewBag.paymentId = new SelectList(db.Payments, "PaymentId", "PaymentName");
            return View();
        }

        // POST: Agents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AgentId,AgentName,Email,Address,Phone,isActivate,Password,ConfirmPassword,Introduction,EmailHide,paymentId,UserId")] Agent agent)
        {
            if (ModelState.IsValid)
            {
                var check = db.Agents.FirstOrDefault(a => a.Email == agent.Email);
                if (check == null)
                {
                    agent.Password = GetMD5(agent.Password);
                    agent.ConfirmPassword = agent.ConfirmPassword;                    
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Agents.Add(agent);
                    db.SaveChanges();
                    return RedirectToAction("Login", "Agents");
                }
                else
                {
                    ViewBag.error = "Email already existed";
                    return View();
                }
            }

            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", agent.UserId);
            ViewBag.paymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", agent.paymentId);
            return View(agent);
        }

        // GET: Agents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", agent.UserId);
            ViewBag.paymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", agent.paymentId);
            return View(agent);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Agent agent)
        {
            ModelState.Remove("Password");
            ModelState.Remove("ConfirmPassword");
            if (ModelState.IsValid)
            {
                var data = db.Agents.Find(agent.AgentId);
                agent.Password = data.Password;
                agent.ConfirmPassword = agent.Password;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(data).CurrentValues.SetValues(agent);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", agent.UserId);
            ViewBag.PaymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", agent.UserId);
            return View(agent);
        }

        // GET: Agents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agent agent = db.Agents.Find(id);
            if (agent == null)
            {
                return HttpNotFound();
            }
            return View(agent);
        }

        // POST: Agents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Agent agent = db.Agents.FirstOrDefault(t => t.AgentId == id);
            foreach (var item in agent.Advertisements.ToList())
            {
                foreach (var image in item.Images.ToList())
                {
                    db.Images.Remove(image);
                }
                db.Advertisements.Remove(item);
            }
            db.Agents.Remove(agent);
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
            var id = Session["AgentId"];
            if (id != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Agents");
            }
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
                var data = db.Agents.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).FirstOrDefault();
                if (data != null)
                {
                    if (data.isActivate == false)
                    {
                        ViewBag.Message = "Please wait for your account to be activated";
                        return View();
                    }
                    else
                    {
                        //add session
                        Session["AgentId"] = data.AgentId;
                        Session["AgentName"] = data.AgentName;
                        Session["Email"] = data.Email;
                        Session["Introduction"] = data.Introduction;
                        Session["Phone"] = data.Phone;
                        Session["Address"] = data.Address;
                        Session["EmailHide"] = Enum.GetName(typeof(EmailHide), data.EmailHide);
                        Session["isActivate"] = Enum.GetName(typeof(isActivate), data.isActivate);
                        Session["paymentId"] = data.paymentId;
                        return RedirectToAction("Information", "Agents");
                    }
                }
                else
                {
                    ViewBag.Message = "Wrong email or password";
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        //create a string MD5
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
