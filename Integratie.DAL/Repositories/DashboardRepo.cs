﻿using Integratie.DAL.EF;
using Integratie.Domain.Entities.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integratie.DAL.Repositories
{
    public class DashboardRepo
    {
        DashBoardDbContext context = new DashBoardDbContext();
        public List<DashboardItem> GetAllDashboardItems()
        {
            return context.Dashboarditems.ToList();
        }

    }
}
