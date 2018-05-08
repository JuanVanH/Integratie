﻿using Integratie.DAL;
using Integratie.DAL.Repositories;
using Integratie.Domain.Entities.Dashboard;
using Integratie.Domain.Entities.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integratie.BL.Managers
{
    public class DashboardManager
    {
        DashboardRepo repo = new DashboardRepo();
        public List<DashboardItem> GetAllDashboardItems()
        {
            List<DashboardItem> dbis = repo.GetAllDashboardItems();
            foreach(DashboardItem dbi in dbis)
            {
                if(dbi.Graph.GetType() == typeof(BarChartGraph))
                {
                    BarChartGraph BCG = (BarChartGraph)dbi.Graph;
                    try
                    {
                        if (dbi.Id == 1)
                        {
                            BCG.Values.Add("Bart", 20);
                            BCG.Values.Add("Stef", 40);
                            BCG.Values.Add("Ben", 10);
                            BCG.Values.Add("Fred", 5);
                        }
                        else
                        {
                            BCG.Values.Add("Bart", 10);
                            BCG.Values.Add("Stef", 20);
                            BCG.Values.Add("Ben", 5);
                            BCG.Values.Add("Fred", 15);
                        }
                    }
                    catch(Exception ex)
                    {
                    }
                }
                if(dbi.Graph.GraphType == GraphType.Single)
                {
                    if (dbi.Graph.PersonFilter != null)
                        dbi.Graph.SingleValue = dbi.Graph.PersonFilter.Count;
                    else dbi.Graph.SingleValue = 7;
                }
            }
            return dbis;
        }
        public List<DashboardItem> Update(List<DashboardItem> dbis)
        {
            return repo.Update(dbis);
        }
        public DashboardItem AddDashboardItem(DashboardItem dbi)
        {
            return repo.AddDashboardItem(dbi);
        }
        public void UpdateDashboard(List<DashboardItem> dashboardItems)
        {
            GraphManager graphManager = new GraphManager();
            if (dashboardItems != null && dashboardItems.Count > 0)
            {
                dashboardItems.ForEach(d => d.Graph = graphManager.GetGraphbyId(d.GraphId));
                Update(dashboardItems);
            }
            else
            {
                repo.ClearItems();
            }
        }
    }
}
