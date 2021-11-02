using PagedList;
using Project_Real__estate.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers;
using System.Web.Mvc;


namespace Project_Real__estate.Controllers
{
    public class HomeController : Controller
    {
        private projectEntities1 db = new projectEntities1();
        public ActionResult Index()
        {
            var advertisements = db.Advertisements.Include(a => a.Agent).Include(a => a.Category).Include(a => a.Payment).Include(a => a.Seller).Include(a => a.User).Where(a => a.isActivate == true);
            return View(advertisements.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult NewPost()
        {
            var agentId = Session["AgentId"];
            var sellerId = Session["SellerId"];
            if (agentId != null || sellerId != null)
            {
                ViewBag.AgentId = Session["AgentId"];
                ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
                ViewBag.PaymentId = new SelectList(db.Payments, "PaymentId", "PaymentName");
                ViewBag.AgentId = Session["SellerId"];
                ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
                ViewBag.Type = new List<SelectListItem>()
            {
                new SelectListItem() { Value="Bán", Text= "Bán" },
                new SelectListItem() { Value="Thuê", Text= "Thuê" },
                new SelectListItem() { Value="Dự Án", Text= "Dự Án" }
             };
            }
            else
            {
                return RedirectToAction("Login", "RegisterLoginView");
            }
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewPost([Bind(Include = "adsId,Tiltle,ReleaseDate,ExpirationDate,SellerId,AgentId,PaymentId,CategoryId,Describe,CurrentSymbol,priceOfAds,EstatePrice,Facade,Gateway,floors,Bedrooms,Toilets,furniture,Area,Cityprovince,District,Ward,Street,isActivate,UserId,StatusHouse")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                List<Image> imglist = new List<Image>();
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var random = Guid.NewGuid() + fileName;
                        var ext = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
                        if (ext.ToLower() == "jpeg" || ext.ToLower() == "jpg" || ext.ToLower() == "png")
                        {
                            Image img = new Image()
                            {
                                FileName = random,
                                Extension = Path.GetExtension(fileName)
                            };
                            imglist.Add(img);

                            var path = Path.Combine(Server.MapPath("~/Images/"), img.FileName);
                            file.SaveAs(path);


                            WebImage imgSize = new WebImage(file.InputStream);
                            if (imgSize.Width > 200)
                                imgSize.Resize(200, 200);
                            imgSize.Save(path);
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "File Extension Is InValid - Only Upload JPG/JPEG/PNG File";
                            return View();
                        }
                    }
                }
                if (Session["AgentId"] != null)
                {
                    advertisement.AgentId = Convert.ToInt32(Session["AgentId"]);
                    advertisement.SellerId = null;
                }
                else if (Session["SellerId"] != null)
                {
                    advertisement.SellerId = Convert.ToInt32(Session["SellerId"]);
                    advertisement.AgentId = null;
                }

                advertisement.Reports = new List<Report>() {
                    new Report() { ReportDate= DateTime.Now, AdsId = advertisement.adsId, AgentId = advertisement.AgentId, SellerId = advertisement.SellerId, Price = advertisement.priceOfAds } };

                advertisement.Images = imglist;
                db.Advertisements.Add(advertisement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AgentId = new SelectList(db.Agents, "AgentId", "AgentName", advertisement.AgentId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", advertisement.CategoryId);
            ViewBag.PaymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", advertisement.PaymentId);
            ViewBag.SellerId = new SelectList(db.Sellers, "SellerId", "Name", advertisement.SellerId);
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", advertisement.UserId);
            ViewBag.Type = new List<SelectListItem>()
            {
                new SelectListItem() { Value="Bán", Text= "Bán" },
                new SelectListItem() { Value="Thuê", Text= "Thuê" },
                new SelectListItem() { Value="Dự Án", Text= "Dự Án" }
             };
            return View(advertisement);
        }


        public ActionResult ListPost(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "Title_desc" : "";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var advertisements = db.Advertisements.Include(a => a.Agent).Include(a => a.Category).Include(a => a.Payment).Include(a => a.Seller).Include(a => a.User).Where(a => a.isActivate == true);
            //status
            if (!String.IsNullOrEmpty(searchString))
                advertisements = advertisements.Where(s => s.StatusHouse.Contains(searchString));
            //type
            if (!String.IsNullOrEmpty(searchString))
                advertisements = advertisements.Where(s => s.StatusHouse.Contains(searchString));
            // area
            if (!String.IsNullOrEmpty(searchString))
                advertisements = advertisements.Where(s => s.StatusHouse.Contains(searchString));
            //city
            if (!String.IsNullOrEmpty(searchString))
                advertisements = advertisements.Where(s => s.StatusHouse.Contains(searchString));
            //bedrom
            if (!String.IsNullOrEmpty(searchString))
                advertisements = advertisements.Where(s => s.StatusHouse.Contains(searchString));
            // bath
            if (!String.IsNullOrEmpty(searchString))
                advertisements = advertisements.Where(s => s.StatusHouse.Contains(searchString));
            switch (sortOrder)
            {
                case "Title_desc":
                    advertisements = advertisements.OrderByDescending(s => s.Tiltle);
                    break;
                case "Price":
                    advertisements = advertisements.OrderBy(s => s.EstatePrice);
                    break;
                case "Price_desc":
                    advertisements = advertisements.OrderByDescending(s => s.EstatePrice);
                    break;
                case "Sell":
                    advertisements = advertisements.Where(s => s.StatusHouse.Equals("Sale")).OrderBy(s => s.Tiltle);
                    break;
                case "Rent":
                    advertisements = advertisements.Where(s => s.StatusHouse.Equals("Rent")).OrderBy(s => s.Tiltle);
                    break;
                default:  // Name ascending 
                    advertisements = advertisements.OrderBy(s => s.Tiltle);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);

            return View(advertisements.ToPagedList(pageNumber, pageSize));

        }
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
            var images = new List<Image>();
            images = db.Images.Where(x => x.AdsId == id).ToList();
            ViewBag.file = images;
            return View(advertisement);
        }

        public ActionResult AgentEdit(int? id)
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
            ViewBag.paymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", agent.paymentId);
            return View(agent);
        }

        // POST: Agents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AgentEdit(Agent agent)
        {
            if (ModelState.IsValid)
            {
                var data = db.Agents.Find(agent.AgentId);
                agent.Password = GetMD5(agent.Password);
                agent.ConfirmPassword = agent.Password;
                agent.isActivate = data.isActivate;
                agent.UserId = data.UserId;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(data).CurrentValues.SetValues(agent);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            ViewBag.paymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", agent.paymentId);
            return View(agent);
        }
        public ActionResult SellerEdit(int? id)
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

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SellerEdit(Seller seller)
        {
            if (ModelState.IsValid)
            {
                var data = db.Sellers.Find(seller.SellerId);
                seller.Password = GetMD5(seller.Password);
                seller.ConfirmPassword = seller.Password;
                seller.isActivate = data.isActivate;
                seller.UserId = data.UserId;
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(data).CurrentValues.SetValues(seller);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(seller);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index");
        }

        public ActionResult AgentDetails(int? id)
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

        public ActionResult SellerDetails(int? id)
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

        public ActionResult AdvertisementEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Advertisement ad = db.Advertisements.Find(id);
            if (ad == null)
            {
                return HttpNotFound();
            }
            return View(ad);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdvertisementEdit(Advertisement advertisement)
        {
            ModelState.Remove("isActivate");
            ModelState.Remove("UserId");
            if (ModelState.IsValid)
            {
                var data = db.Advertisements.Find(advertisement.adsId);
                advertisement.AgentId = data.AgentId;
                advertisement.SellerId = data.SellerId;

                db.Configuration.ValidateOnSaveEnabled = false;
                db.Entry(data).CurrentValues.SetValues(advertisement);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(advertisement);
        }
    }
}