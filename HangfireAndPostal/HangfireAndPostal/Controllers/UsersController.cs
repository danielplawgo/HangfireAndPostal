using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Hangfire;
using HangfireAndPostal.Models;
using Postal;

namespace HangfireAndPostal.Controllers
{
    public class UsersController : Controller
    {
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid == false)
            {
                return View(user);
            }

            BackgroundJob.Enqueue(() => SendEmail(user));

            return Content("Added");
        }

        public void SendEmail(User user)
        {
            var email = new RegisterUserEmail()
            {
                FirstName = user.FirstName,
                Email = user.Email
            };

            var viewsPath = Path.GetFullPath(HostingEnvironment.MapPath(@"~/Views/Emails"));
            var engines = new ViewEngineCollection();
            engines.Add(new FileSystemRazorViewEngine(viewsPath));

            var emailService = new EmailService(engines);

            emailService.Send(email);
        }
    }
}