﻿using Integratie.BL.Managers;
using Integratie.Domain.Entities.Dashboard;
using Integratie.Domain.Entities.Graph;
using Integratie.Domain.Entities.Subjects;
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
        public ActionResult Index()
        {
            ViewBag.Message = "Your contact page.";
            List<double> listValues = new List<double>();
            List<string> listKeys = new List<string>();
            BarChartGraph graph = (BarChartGraph)manager.GetAllFilledGraphs().First();
            graph.Values.Values.ToList().ForEach(i => listValues.Add(i));
            graph.Values.Keys.ToList().ForEach(i => listKeys.Add(i));
            ViewBag.DataValues = JsonConvert.SerializeObject(listValues);
            ViewBag.DataKeys = JsonConvert.SerializeObject(listKeys);
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
        public ActionResult Upload_file(HttpPostedFileBase file)
        {
            if(file != null && file.ContentLength > 0)
            {
                try
                {
                    string path = Path.Combine(Server.MapPath("~/Upload"), Path.GetFileName(file.FileName));
                    string pad = "C:\\Users\\yanni\\OneDrive\\Documenten\\Yannis School\\P2\\NET\\lol";
                    file.SaveAs(pad);
                    ViewBag.Message = "file uploaded succesfully";
                }catch(Exception ex)
                {
                    ViewBag.Message = "ERROR, file not uploaded";
                }
            }
            else
            {
                ViewBag.Message = "Please select file";
            }
            return View();
        }

        public ActionResult Gemeente()
        {
            ViewBag.Message = "Your Towns Page";
            string[] gemeentes = { "AALST",
"AARTSELAAR",
"ANDERLECHT",
"ANTWERPEN",
"BELSELE",
"BERCHEM",
"BORGERHOUT",
"BORSBEKE",
"BOUWEL",
"BRASSCHAAT",
"BRUSSEL",
"DE PANNE",
"DESSEL",
"DEURNE",
"DIEPENBEEK",
"DUFFEL",
"EDEGEM",
"EKEREN",
"GANSHOREN",
"GEEL",
"GENK",
"GENT",
"HARELBEKE",
"HEUSDEN",
"HOBOKEN",
"HOEILAART",
"JETTE",
"KACHTEM",
"KALMTHOUT",
"KAPELLEN",
"KESSEL LO",
"KOERSEL",
"KOOLKERKE",
"KORTRIJK",
"KRAAINEM",
"KRUISHOUTEM",
"LEDEBERG",
"LEEFDAAL",
"LEOPOLDSBURG",
"LEUVEN",
"LIEDEKERKE",
"LIER",
"LOKEREN",
"LOMMEL",
"LUBBEEK",
"MALDEGEM",
"MEERBEEK",
"MEERLE",
"MEISE",
"MELLE",
"MERCHTEM",
"MERKSPLAS",
"MOL",
"MORTSEL",
"NEDEROKKERZEEL",
"NEDER-OVER-HEEMBEEK",
"NIEUWERKERKEN",
"NOORDERWIJK",
"OETINGEN",
"OPWIJK",
"OUDERGEM",
"OUD-TURNHOUT",
"POEKE",
"RAMSDONK",
"RAMSEL",
"REKKEM",
"ROLLEGEM",
"ROTSELAAR",
"RUISBROEK",
"RUMBEKE",
"SCHAARBEEK",
"SCHELDERODE",
"SCHILDE",
"SCHULEN",
"SINT-AGATHA-RODE",
"SINT-AMANDSBERG",
"SINT-GILLIS-DENDERMONDE",
"SINT-JAN",
"SINT-JANS-MOLENBEEK",
"SINT-JOOST-TEN-NOODE",
"SINT-LAMBRECHTS-HERK",
"SINT-MARTENS-LATEM",
"SINT-MARTENS-LENNIK",
"SINT-NIKLAAS",
"SINT-PAUWELS",
"SINT-PIETERS-WOLUWE",
"SINT-ULRIKS-KAPELLE",
"SLEIDINGE",
"STEENHUFFEL",
"STOKKEM",
"STOKROOIE",
"TIELEN",
"TORHOUT",
"UITBERGEN",
"UKKEL",
"VELDWEZELT",
"VELM",
"VILVOORDE",
"VISSENAKEN",
"VLISSEGEM",
"VOORDE",
"VOSSEM",
"VRASENE",
"WALEM",
"WALTWILDER",
"WELDEN",
"WESPELAAR",
"WESTKAPELLE",
"WIDOOIE",
"WIEKEVORST",
"WIEZE",
"WIJSHAGEN",
"WOMMELGEM",
"WULPEN",
"ZANDVOORDE",
"ZARLARDINGE",
"ZAVENTEM",
"ZEGELSEM",
"ZELE",
"ZEPPEREN",
"ZERKEGEM",
"ZEVEREN",
"ZICHEN-ZUSSEN-BOLDER",
"ZOERLE-PARWIJS",
"ZOERSEL",
"ZOLDER",
"ZONHOVEN",
"ZOTTEGEM" };
            return View(gemeentes);
        }

        public ActionResult GemeentePage(string gemeente) {
            SubjectManager subjectmngr = new SubjectManager();
            IEnumerable<Subject> people=subjectmngr.GetPeopleByTown(gemeente);
            return View(people);
        }

    }
}