using Job_Offers_WebSite3.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var list = db.Categories.ToList();

            return View(list);
        }

        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);
            if (job == null)
            {
                return HttpNotFound();

            }
            Session["JobId"] = JobId;
            return View(job);
        }
        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var UserId = User.Identity.GetUserId();
            var JobId = (int)Session["JobId"];

            var check = db.ApplyForJobs.Where(a => a.JobId == JobId && a.UserId == UserId).ToList();


            if (check.Count < 1)
            {
                var job = new ApplyForJob();
                job.UserId = UserId;
                job.JobId = JobId;
                job.Message = Message;
                job.ApplyDate = DateTime.Now;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "تمت الإضافة بنجاح !";
            }
            else
            {
                ViewBag.Result = "  المعذرة لقد سبق وتقدمت الي نفس الوظيفة  ";

            }


            return View();
        }
        [Authorize]
        public ActionResult GetJobsByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Jobs = db.ApplyForJobs.Where(a => a.UserId == UserId);
            return View(Jobs.ToList());
        }
        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();

            }
            return View(job);
        }

        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserID = User.Identity.GetUserId();
            var Jobs = from app in db.ApplyForJobs join job in db.Jobs on app.JobId equals job.JobId where job.User.Id == UserID select app;

            var groupped = from j in Jobs
                           group j by j.job.JobTitle into gr
                           select new JobsViewModels
                           {
                               JobTitle = gr.Key,
                               Items = gr

                           };
            return View(groupped.ToList());
        }
        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);

        }
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            if (ModelState.IsValid)
            {
                job.ApplyDate = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            return View(job);
        }

        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
            try
            {
                // TODO: Add delete logic here
                var myJob = db.ApplyForJobs.Find(job.Id);
                db.ApplyForJobs.Remove(myJob);
                db.SaveChanges();
                return RedirectToAction("GetJobsByUser");
            }
            catch
            {
                return View(job);
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
    

            return View();
        }
        [HttpPost]
        public ActionResult Contact(ContactModel contact)
        {
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("i.yagoub2019@gmail.com", "calpvwtyarhaizuo");
            mail.From = new MailAddress(contact.Email);
            mail.To.Add(new MailAddress("i.yagoub2019@gmail.com"));
            mail.Subject = contact.Subject;
            mail.IsBodyHtml = true;
            string body = "إسم المرسل" + contact.Name + "<br>" +
                          "بريد المرسل" + contact.Email + "<br>" +
                          "عنوان الرسالة" + contact.Subject + "<br>" +
                          "نص الرسالة:<b>" + contact.Message + "</b >";
            mail.Body = body;

            var smtpClient = new SmtpClient("smtp.gmail.com",587);
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);


            return RedirectToAction("Index");
        }
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchName)
        {
            var result = db.Jobs.Where(a => a.JobTitle.Contains(searchName)
            || a.JobContent.Contains(searchName)
            || a.Category.CategoryName.Contains(searchName)
            || a.Category.CategoryDescription.Contains(searchName)).ToList();
            return View(result);
        }
    }   }