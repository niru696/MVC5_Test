using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication1.Hubs;
using WebApplication1.Models;
using WebApplication1.Util;

namespace WebApplication1.Controllers
{
    [AllowAnonymous]
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(MemberModel model)
        {
            using( var context = new TutorialDBEntities())
            {
                bool isValid = context.User.Any(x => x.UserName == model.UserName && x.Password == model.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Employees");
                }
                ModelState.AddModelError("", "Invalid User or Password");
                return View();
            }
            
        }

        public ActionResult Singup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Singup(User model)
        {
            //ProgressHub.SendMessage("initializing and preparing", 2);
            using (var context = new TutorialDBEntities())
            {
                context.User.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public JsonResult LongRunningProcess()
        {
            //THIS COULD BE SOME LIST OF DATA
            int itemsCount = 100;

            for (int i = 0; i <= itemsCount; i++)
            {
                //SIMULATING SOME TASK
                Thread.Sleep(500);

                //CALLING A FUNCTION THAT CALCULATES PERCENTAGE AND SENDS THE DATA TO THE CLIENT
                Functions.SendProgress("Process in progress...", i, itemsCount);
            }

            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}