﻿using Integratie.Domain;
using Integratie.Domain.Entities;
using Integratie.Domain.Entities.Alerts;
using Integratie.Domain.Entities.Dashboard;
using Integratie.Domain.Entities.Graph;
using Integratie.Domain.Entities.Subjects;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integratie.DAL.EF
{
    public class DashboardDbInitializer : DropCreateDatabaseIfModelChanges<DashBoardDbContext>
    {
        protected override void Seed(DashBoardDbContext context)
        {
            List<Feed> feeds = new List<Feed>();
            IEnumerable<Feed> resultsFeed = JsonConvert.DeserializeObject<IEnumerable<Feed>>(dashBoardDbTextgain.postJson());

            Console.WriteLine("Making " + resultsFeed.Count() + " feeds");
            foreach (var item in resultsFeed)
            {
                feeds.Add(new Feed(new Profile(item.Profile.Gender, item.Profile.Age, item.Profile.Education, item.Profile.Language, item.Profile.Personality), item.Words, item.Sentiment, item.Source, item.Hashtags, item.ID, item.Themes, item.Persons, item.Urls, item.Date, item.Mentions, item.Geo, item.Retweet));
            }

            Console.WriteLine("Adding " + resultsFeed.Count() + " feeds to database");
            foreach (var feed in feeds)
            {
                context.Feeds.Add(feed);
                
            }

            List<Person> people = new List<Person>();
            String st = File.ReadAllText(@"F:\School\Repositories\Integratieproject 1\net\Integratie.DAL\politici.json");
            IEnumerable <Person> resultsPerson = JsonConvert.DeserializeObject<IEnumerable<Person>>(st);
            Console.WriteLine("Making" + resultsFeed.Count() + "people");
            foreach (var item in resultsPerson)
            {
                people.Add(new Person(item.First_Name, item.Last_Name, item.District, item.District, item.Gender, item.Twitter, item.Site, item.DateOfBirth, item.Facebook, item.Postal_Code, item.Full_Name, item.Position, item.Organisation, item.Id, item.Town));
            }
            Console.WriteLine("Adding" + resultsPerson.Count() + "people to database");
            foreach (var person in people)
            {
                context.People.Add(person);
            }
            //DUMMY ALERTS
            CheckAlert CH1 = new CheckAlert(null, SubjectProperty.relativeCount, Operator.GT, 1.5, S1);
            CheckAlert CH2 = new CheckAlert(null, SubjectProperty.relativeCount, Operator.GT, 2, S2);
            CheckAlert CH3 = new CheckAlert(null, SubjectProperty.relativeCount, Operator.GT, 2, S3);
            CheckAlert CH4 = new CheckAlert(null, SubjectProperty.relativeCount, Operator.GT, 1, S4);
            CompareAlert CO1 = new CompareAlert(S1, S2, Operator.GT);
            CompareAlert CO2 = new CompareAlert(S1, S2, Operator.LT);
            TrendAlert TR1 = new TrendAlert(S1);

            context.Alerts.Add(CH1);
            context.Alerts.Add(CH2);
            context.Alerts.Add(CH3);
            context.Alerts.Add(CH4);
            context.Alerts.Add(CO1);
            context.Alerts.Add(CO2);
            context.Alerts.Add(TR1);

            //ACCOUNTS
            Account A1 = new Account(0, "JanVH", "jvanhoye@hotmail.com");
            

            //GRAPHS
            BarChartGraph BCG1 = new BarChartGraph(new List<Subject> { S1, S2 }, A1);
            BarChartGraph BCG2 = new BarChartGraph(new List<Subject> { S3, S4 }, A1);
            context.Graphs.Add(BCG1);
            context.Graphs.Add(BCG2);

            //DashboardItems
            DashboardItem DBI1 = new DashboardItem(0, 1, 2, 3, 3, BCG1);
            //DashboardItem DBI1 = new DashboardItem(1,)
            context.SaveChanges();
        }
    }
}
