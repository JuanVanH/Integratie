﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integratie.BL.Managers.Interfaces
{
    public interface IMailManager
    {
        void SendMail(string Body,string adress,string name);
    }
}
