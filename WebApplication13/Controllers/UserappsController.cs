using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication13.Models;
using System.Web.Http;
using static System.Collections.Specialized.BitVector32;
using HttpPostAttribute = System.Web.Mvc.HttpPostAttribute;
using ActionNameAttribute = System.Web.Mvc.ActionNameAttribute;
using System.Web.Http.Results;

namespace WebApplication13.Controllers
{
    public class UserappsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Userapps
        public ActionResult Index()
        {
            return View(db.userapp.ToList());
        }

        // GET: Userapps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Userapp userapp = db.userapp.Find(id);
            if (userapp == null)
            {
                return HttpNotFound();
            }
            return View(userapp);
        }

        // GET: Userapps/register
        public ActionResult register()
        {
            return View();
        }

        // POST: Userapps/register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult register([Bind(Include = "userId,name,Email,Password,phone,country,image")] Userapp userapp, HttpPostedFileBase file)
        {
           IHttpActionResult user = new UserappsapiController().PostRejester(userapp);
            HomeController home = new HomeController();
            home.FileUpload( file);
            var x = user as OkNegotiatedContentResult<Error>;
            if (x.Content.Status!=0)
            {
                if (x.Content.Status == 1)
                {
                    Session["userRole"] = "admin";
                }
                if (x.Content.Status == 2)
                {
                    Session["userRole"] = "user";
                }
                Session["userRole"] = x.Content.userapps.uR.ToString();
                return RedirectToAction("Index", "products");

            }

            return View(userapp);
        }

        // GET: Userapps/register
        public ActionResult logout()
        {
            Session.Remove("userRole");
            return RedirectToAction("Index", "Home");
        }



        // GET: Userapps/register
        public ActionResult login()
        {
            return View();
        }

        // POST: Userapps/register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(string Password, string Email)
        {
            if (Email != null || Password != null)
            {
                LoginModel login = new LoginModel();
                login.Email = Email;
                login.Password = Password;
                IHttpActionResult user = new UserappsapiController().Postlogin(login);

                var x = user as OkNegotiatedContentResult<Error>;
                if (x.Content.Status != 0) { 
                    if(x.Content.Status == 1)
                    {
                        Session["userRole"] ="admin";
                    }
                    else if (x.Content.Status == 2)
                    {
                        Session["userRole"] = "user";
                    }
                    Session["userRole1"] = x.Content.userapps.uR.ToString();
                    ViewBag.role= x.Content.userapps.uR; 
                    return RedirectToAction("Index","products");
                }

             
            }

            return View();
        }

        // GET: Userapps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Userapp userapp = db.userapp.Find(id);
            if (userapp == null)
            {
                return HttpNotFound();
            }
            return View(userapp);
        }

        // POST: Userapps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userId,name,Email,Password,phone,country,image,uR")] Userapp userapp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userapp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userapp);
        }

        // GET: Userapps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Userapp userapp = db.userapp.Find(id);
            if (userapp == null)
            {
                return HttpNotFound();
            }
            return View(userapp);
        }

        // POST: Userapps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Userapp userapp = db.userapp.Find(id);
            db.userapp.Remove(userapp);
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
