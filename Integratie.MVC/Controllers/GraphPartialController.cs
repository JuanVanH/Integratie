﻿using Integratie.BL.Managers;
using Integratie.Domain.Entities.Alerts;
using Integratie.Domain.Entities.Graph;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Integratie.MVC.Controllers
{
    public class GraphPartialController : TeslaBaseController
    {
        GraphManager graphManager = new GraphManager();
        // GET: GraphPartial
        public ActionResult _BarChart(Alert alert)
        {
            alert.Graph.BarValues = JsonConvert.DeserializeObject<Dictionary<string,double>>(alert.JsonValues);
            return PartialView(alert);
        }

        public ActionResult _LineChart(Alert alert)
        {
            alert.Graph.LineValues = JsonConvert.DeserializeObject<Dictionary<string, List<double>>>(alert.JsonValues);
            return PartialView(alert);
        }

        public ActionResult _PieChart(Alert alert)
        {
            //ViewBag.id = id;
            //PieGraph graph = graphManager.GetPieGraph(id);
            //List<double> listValues = graph.Values.Values.ToList();
            //List<string> listKeys = graph.Values.Keys.ToList();
            //ViewBag.Values = JsonConvert.SerializeObject(listValues);
            //ViewBag.Keys = JsonConvert.SerializeObject(listKeys);
            return PartialView();
        }
    }
}