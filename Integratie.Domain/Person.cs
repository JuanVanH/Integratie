﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integratie.Domain
{
    public class Person : Subject
    {
        public Person(int iD, string name) : base(iD, name)
        {
        }
    }
}
