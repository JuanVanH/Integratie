﻿using Integratie.BL.Managers;
using Integratie.Domain.Entities;
using Integratie.MVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static Integratie.MVC.Controllers.ManageController;

namespace Integratie.MVC.Controllers
{
    public class SuperAdminController : Controller
    {
        private AccountManager mgr;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext context;
        // GET: SuperAdmin

        public SuperAdminController()
        {
            mgr = new AccountManager();
            context = new ApplicationDbContext();
        }

        public SuperAdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        #region managers
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        #endregion

        public ActionResult GetAdmins(string roleName = "Admin")
        {
            var roleManager =
                new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            IQueryable<ApplicationUser> users = null;
            if (roleManager.RoleExists("Admin"))
            {
                var idsWithPermission = roleManager.FindByName("Admin").Users.Select(iur => iur.UserId);
                users = context.Users.Where(u => idsWithPermission.Contains(u.Id));
            }
            return View(users);
        }

        // GET: /SuperAdmin/ChangePassworOfAdmin
        public ActionResult EditAdminPassword(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangePasswordAdminViewModel model = new ChangePasswordAdminViewModel() { Id = id };
            return View(model);
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditAdminPassword(ChangePasswordAdminViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var removePassword = UserManager.RemovePassword(model.Id);
            if (removePassword.Succeeded)
            {
                //Removed Password Success
                var AddPassword = UserManager.AddPassword(model.Id, model.NewPassword);
                if (AddPassword.Succeeded)
                {
                    return View();
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult EditEmail(string id, string email)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChangeEmailViewModel model = new ChangeEmailViewModel() { Id = id};
            return View(model); 
        }

        [HttpPost]
        public ActionResult EditEmail(ChangeEmailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var changeEmail = UserManager.SetEmail(model.Id, model.NewEmail);
            return View();
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        public void ExportToCSV()
        {
            StringWriter sw = new StringWriter();
            Response.ClearContent();
            Response.AddHeader("content-disposition", "attachement; filename=ExportedUsersList.csv");
            Response.ContentType = "text/csv";

            var users = mgr.GetAccounts();

            foreach (var item in users)
            {
                sw.WriteLine(String.Format("\"{0}\",\"{1}\",\"{2}\",\"{3}\"", item.ID, item.Name, item.Mail, item.Password));
            }
            Response.Write(sw.ToString());
            Response.End();
        }

    }
}