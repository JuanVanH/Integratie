﻿using Integratie.Domain.Alerts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integratie.DAL
{
    public interface IAlertRepo
    {
        List<Alert> GetAlerts();
        Alert GetAlertById(int ID);
        void RemoveAlert(Alert alert);
        Alert AddAlert(Alert alert);

    }
}
