﻿using Integratie.BL.Managers;
using Integratie.Domain.Entities.Dashboard;
using Integratie.Domain.Entities.Graph;
using Integratie.Domain.Entities.Subjects;
using Integratie.MVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Integratie.MVC.Controllers
{
    public class HomeController : Controller
    {
        GraphManager manager = new GraphManager();
        DashboardManager dbmanager = new DashboardManager();
        SubjectManager subjectManager = new SubjectManager();
        FeedManager feedManager = new FeedManager();
        public ActionResult Index()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.persons = subjectManager.GetPersonen().Select(p => p.Full_Name);
            List<DashboardItem> dbitems = dbmanager.GetAllDashboardItems();
            return View(dbitems);
        }

        [HttpPost]
        public ActionResult Index(List<DashboardItem> dbis)
        {
            dbmanager.UpdateDashboard(dbis);
            List<DashboardItem> dbitems = dbmanager.GetAllDashboardItems();
            //foreach (DashboardItem dbi in dbis)
            //{
            //    dbi.Graph = dbitems.ToList().Where(e => e.Id == dbi.Id).First().Graph;
            //}
            //dbmanager.Update(dbis);
            //dbitems = dbmanager.GetAllDashboardItems();

            ViewBag.persons = subjectManager.GetPersonen().Select(p => p.Full_Name);
            return View(dbitems);
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
        public ActionResult AddGraph(Graph graph)
        {
            DashboardItem dbi = new DashboardItem(0, 1, 1000, 3, 2, graph);
            dbmanager.AddDashboardItem(dbi);
            List<DashboardItem> dbitems = dbmanager.GetAllDashboardItems();
            return View(dbitems);
        }
        public ActionResult Search(String zoek)
        {
            Search search = new Search();
            search.persons = subjectManager.GetPeopleByName(zoek);
            search.organisaties = subjectManager.GetOrganisaties(zoek);
            search.steden = subjectManager.GetGemeentes(zoek);
            search.feeds = feedManager.GetWordFeeds(zoek);
            search.feedsByPerson = feedManager.GetPersonFeeds(zoek);
            return View(search);
        }
    }
}