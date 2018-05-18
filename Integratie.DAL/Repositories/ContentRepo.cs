﻿using Integratie.DAL.EF;
using Integratie.Domain.Entities.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integratie.DAL.Repositories
{
    
    public class ContentRepo
    {
        DashBoardDbContext context = new DashBoardDbContext();
        public SiteContent GetContextById(string id)
        {
            return context.SiteContents.Where(sc => sc.IdKey == id).First();
        }
        public SiteContent AddSiteContent(SiteContent sc)
        {
            try
            {
                SiteContent sc2 = context.SiteContents.Add(sc);
                context.SaveChanges();
                return sc2;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public bool DeleteSiteContent(SiteContent sc)
        {
            try
            {
                context.SiteContents.Remove(sc);
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        public bool UpdateSiteContent(SiteContent sc)
        {
            try
            {
                context.Entry(sc).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
