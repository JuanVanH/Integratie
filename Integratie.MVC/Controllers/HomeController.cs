﻿using Integratie.BL.Managers;
using Integratie.Domain.Entities.Dashboard;
using Integratie.Domain.Entities.Graph;
using Integratie.Domain.Entities.Subjects;
using Integratie.MVC.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Integratie.MVC.Controllers
{
    public class HomeController : TeslaBaseController
    {
        GraphManager manager = new GraphManager();
        DashboardManager dbmanager = new DashboardManager();
        SubjectManager subjectManager = new SubjectManager();
        FeedManager feedManager = new FeedManager();
        ThemeManager themeManager = new ThemeManager();
        AlertManager alertManager = new AlertManager();
        AccountManager accountManager = new AccountManager();
        public ActionResult Index()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.persons = subjectManager.GetPersonen().Select(p => p.Full_Name);
            ViewBag.partijen = subjectManager.GetOrganisaties();
            ViewBag.themas = themeManager.GetThemas().Select(t => t.Name);
            List<DashboardItem> dbitems = dbmanager.GetAllDashboardItems();
            return View(dbitems);
        }

        [HttpPost]
        public RedirectResult Index(List<DashboardItem> dbis)
        {
            dbmanager.UpdateDashboard(dbis);
            List<DashboardItem> dbitems = dbmanager.GetAllDashboardItems();
            return Redirect("Index");
        }
       [HttpPost]
        public ActionResult RemoveDashboardItem(DashboardItem dbi)
        {
            dbmanager.RemoveDashboardItem(dbi);
            List<DashboardItem> dbitems = dbmanager.GetAllDashboardItems();
            ViewBag.persons = subjectManager.GetPersonen().Select(p => p.Full_Name);
            return View(dbitems);
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
        public ActionResult CSV()
        {
            return View();
        }
        [HttpPost]
        public RedirectResult AddGraph(Graph graph)
        {
            DashboardItem dbi;
            if (graph.GraphType == GraphType.Single || graph.GraphType == GraphType.SingleTrend) dbi = new DashboardItem(0, 1, 1000, 8, 5, graph);
            else dbi = new DashboardItem(0, 1, 1000, 15, 9, graph);
            dbmanager.AddDashboardItem(dbi);
            List<DashboardItem> dbitems = dbmanager.GetAllDashboardItems();
            return Redirect("Index");
        }
        public ActionResult Search(String zoek)     
        {
            Search search = new Search();
            search.persons = subjectManager.GetPeopleByName(zoek);
            search.organisaties = subjectManager.GetOrganisaties(zoek);
            search.steden = subjectManager.GetGemeentes(zoek);
            search.feeds = feedManager.GetWordFeeds(zoek);
            search.feedsByPerson = feedManager.GetPersonFeeds(zoek);
            search.themas = themeManager.GetThemas();
            return View(search);
        }

        public ActionResult Alerts()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddUserAlert(AlertCreation alert)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Alerts", "Home", new { });

            try
            {
                alertManager.AddUserAlert(User.Identity.GetUserId(), alert.AlertType, alert.Subject, alert.Web, alert.Mail, alert.App, alert.SubjectB, alert.Compare, alert.SubjectProperty, alert.Value);
            }
            catch (Exception)
            {
                return Json(new { Url = redirectUrl, status = "Error" });
            }

            return Json(new { Url = redirectUrl, status = "OK" });
        }

        [HttpPost]
        public void UpdateUserAlert(AlertUpdate alert)
        {
            alertManager.UpdateUserAlert(alert.Id, alert.Web, alert.Mail, alert.App);
        }

        [HttpPost]
        public void RemoveUserAlert(int id)
        {
            alertManager.RemoveUserAlert(id);
        }

        public ActionResult Follows()
        {
            return View(accountManager.GetAccountById(User.Identity.GetUserId()).Follows);
        }
    }
}