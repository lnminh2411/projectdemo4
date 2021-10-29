using Project_Real__estate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Project_Real__estate.Controllers
{
    public class RegisterLoginViewController : Controller
    {
        private projectEntities1 db = new projectEntities1();
        // GET: RegisterView
        
        public ActionResult Register(AgentSellerView rv)
        {
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            ViewBag.paymentId = new SelectList(db.Payments, "PaymentId", "PaymentName");
            return View(new AgentSellerView { Agent = new Agent(), Seller = new Seller() });
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterAgent(AgentSellerView rv)
        {
            if (ModelState.IsValid)
            {
                var check = db.Agents.FirstOrDefault(a => a.Email == rv.Agent.Email);
                if (check == null)
                {
                    rv.Agent.Password = GetMD5(rv.Agent.Password);
                    rv.Agent.ConfirmPassword = GetMD5(rv.Agent.ConfirmPassword);
                    rv.Agent.isActivate = false;
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.Agents.Add(rv.Agent);
                    db.SaveChanges();
                    return RedirectToAction("LoginAgent", "RegisterLoginView");
                }
                else
                {
                    ViewBag.error = "Email already existed";
                    return View("Register", rv);
                }
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName", rv.Agent.UserId);
            ViewBag.PaymentId = new SelectList(db.Payments, "PaymentId", "PaymentName", rv.Agent.paymentId);
            return View("Register", rv);
        }
        public ActionResult RegisterSeller(AgentSellerView rv)
        {
            if (ModelState.IsValid)
            {
                var check = db.Sellers.FirstOrDefault(s => s.Email == rv.Seller.Email);
                if (check == null)
                {
                    rv.Seller.Password = GetMD5(rv.Seller.Password);
                    rv.Seller.ConfirmPassword = rv.Seller.ConfirmPassword;
                    rv.Seller.isActivate = false;
                    db.Sellers.Add(rv.Seller);
                    db.SaveChanges();
                    return RedirectToAction("LoginAgent", "RegisterLoginView");
                }
                else
                {
                    ViewBag.error = "Email already existed";
                    return View("Register", rv);
                }
            }
            ViewBag.UserId = new SelectList(db.Users, "UserId", "UserName");
            ViewBag.paymentId = new SelectList(db.Payments, "PaymentId", "PaymentName");
            return View("Register", rv);
        }
        public static string GetMD5(string str)
        {
            if (!String.IsNullOrEmpty(str))
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
            else
                return null;
            
        }

        public ActionResult Login(AgentSellerView rv)
        {
            return View(new AgentSellerView { Agent = new Agent(), Seller = new Seller() });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginAgent(string AEmail, string APassword)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(APassword);
                var data = db.Agents.Where(s => s.Email.Equals(AEmail) && s.Password.Equals(f_password)).FirstOrDefault();
                if (data != null)
                {
                    if (data.isActivate == false)
                    {
                        ViewBag.Message = "Please wait for your account to be activated";
                        return View("Login");
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
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.Message = "Wrong email or password";
                }
            }
            return View("Login");
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginSeller(string SEmail, string SPassword)
        {
            if (ModelState.IsValid)
            {
                var f_password = GetMD5(SPassword);
                var data = db.Sellers.Where(s => s.Email.Equals(SEmail) && s.Password.Equals(f_password)).FirstOrDefault();
                if (data != null)
                {
                    if (data.isActivate == false)
                    {
                        ViewBag.Message = "Please wait for your account to be activated";
                        return View("Login");
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
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ViewBag.Message = "Wrong email or password";
                }
            }

            return View("Login");
        }


    }
}