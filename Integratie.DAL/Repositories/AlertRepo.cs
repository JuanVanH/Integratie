﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Integratie.DAL.EF;
using Integratie.DAL.Repositories.Interfaces;
using Integratie.Domain.Entities.Alerts;

namespace Integratie.DAL.Repositories
{
    public class AlertRepo : IAlertRepo
    {
        private DashBoardDbContext context;

        public AlertRepo()
        {
            context = new DashBoardDbContext();
        }

        public AlertRepo(UnitOfWork uow)
        {
            context = uow.Context;
        }

        public void AddAlert(Alert alert)
        {
            context.Alerts.Add(alert);
            context.SaveChanges();
        }

        public void AddUserAlert(UserAlert userAlert)
        {
            context.UserAlerts.Add(userAlert);
            context.SaveChanges();
        }

        public Alert GetAlertById(int id)
        {
            return context.Alerts.Find(id);
        }

        public IEnumerable<Alert> GetAlerts()
        {
            return context.Alerts.ToList();
        }

        public async Task<List<Alert>> GetAlertsAsync()
        {
            return await context.Alerts.ToListAsync();
        }

        public UserAlert GetUserAlert(int id)
        {
            return (UserAlert)context.UserAlerts.Find(id);
        }

        public UserAlert GetUserAlertByUserAndAlert(string userId, int alertId)
        {
            return context.UserAlerts.Single(u => u.Account.ID.Equals(userId) && u.Alert.AlertID.Equals(alertId));
        }

        public IEnumerable<UserAlert> GetUserAlerts()
        {
            return context.UserAlerts.ToList();
        }

        public IEnumerable<UserAlert> GetUserAlertsOfAlert(int alertId)
        {
            return context.UserAlerts.Where(u => u.Alert.AlertID.Equals(alertId)).ToList<UserAlert>();
        }

        public async Task<List<UserAlert>> GetUserAlertsOfAlertAsync(int alertId)
        {
            return await context.UserAlerts.Where(u => u.Alert.AlertID.Equals(alertId)).ToListAsync();
        }

        public IEnumerable<UserAlert> GetUserAlertsOfUser(string userId)
        {
            return context.UserAlerts.Where(u => u.Account.ID.Equals(userId)).ToList<UserAlert>();
        }

        public IEnumerable<UserAlert> GetUserTrendAlertsOfUser(string userId)
        {
            return context.UserAlerts.Where(u => u.Account.ID.Equals(userId) && u.Alert is TrendAlert);
        }

        public IEnumerable<UserAlert> GetUserCheckAlertsOfUser(string userId)
        {
            return context.UserAlerts.Where(u => u.Account.ID.Equals(userId) && u.Alert is CheckAlert);
        }

        public IEnumerable<UserAlert> GetUserCompareAlertsOfUser(string userId)
        {
            return context.UserAlerts.Where(u => u.Account.ID.Equals(userId) && u.Alert is CompareAlert);
        }

        public IEnumerable<UserAlert> GetUserSentimentAlertsOfUser(string userId)
        {
            return context.UserAlerts.Where(u => u.Account.ID.Equals(userId) && u.Alert is SentimentAlert);
        }

        public UserAlert GetUserWeeklyAlert(string userId)
        {
            return context.UserAlerts.Where(u => u.Account.ID.Equals(userId) && u.Alert is WeeklyAlert).First();
        }

        public void RemoveAlert(Alert alert)
        {
            context.Alerts.Remove(alert);
        }

        public void UpdateAlert(Alert alert)
        {
            context.Entry(alert).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public async Task UpdateAlerts(List<Alert> alerts)
        {
            foreach (Alert alert in alerts)
            {
                context.Entry(alert).State = System.Data.Entity.EntityState.Modified;
            }
            await context.SaveChangesAsync();
        }

        public void UpdateUserAlert(UserAlert userAlert)
        {
            context.Entry(userAlert).State = System.Data.Entity.EntityState.Modified;
            context.SaveChanges();
        }

        public async Task UpdateUserAlerts(List<UserAlert> userAlerts)
        {
            foreach (UserAlert userAlert in userAlerts)
            {
                context.Entry(userAlert).State = System.Data.Entity.EntityState.Modified;
            }
            await context.SaveChangesAsync();
        }

        public CheckAlert FindCheckAlert(CheckAlert alert)
        {
            CheckAlert checkAlert = null;
            try
            {
                checkAlert = context.Alerts.OfType<CheckAlert>().Where(a => a.Subject.ID.Equals(alert.Subject.ID) && a.Operator.Equals(alert.Operator) && a.SubjectProperty.Equals(alert.SubjectProperty) && a.Value.Equals(alert.Value)).First();
            }
            catch (Exception)
            {
                return checkAlert;
            }
            return checkAlert;
        }

        public SentimentAlert FindSentimentAlert(SentimentAlert alert)
        {
            SentimentAlert sentimentAlert = null;
            try
            {
                sentimentAlert = context.Alerts.OfType<SentimentAlert>().Where(a => a.Subject.ID.Equals(alert.Subject.ID) && a.Operator.Equals(alert.Operator) && a.SubjectProperty.Equals(alert.SubjectProperty) && a.Value.Equals(alert.Value)).First();
            }
            catch (Exception)
            {
                return sentimentAlert;
            }
            return sentimentAlert;
        }

        public TrendAlert FindTrendAlert(TrendAlert alert)
        {
            TrendAlert trendAlert = null;
            try
            {
                trendAlert = context.Alerts.OfType<TrendAlert>().Where(a => a.Subject.ID.Equals(alert.Subject.ID)).First();
            }
            catch (Exception)
            {
                return trendAlert;
            }
            return trendAlert;
        }

        public CompareAlert FindCompareAlert(CompareAlert alert)
        {
            CompareAlert compareAlert = null;
            try
            {
                compareAlert = context.Alerts.OfType<CompareAlert>().Where(a => a.SubjectA.ID.Equals(alert.SubjectA.ID) && a.SubjectB.ID.Equals(alert.SubjectB.ID) && a.Operator.Equals(alert.Operator)).First();
            }
            catch (Exception)
            {
                return compareAlert;
            }
            return compareAlert;
        }

        public WeeklyAlert FindWeeklyAlert()
        {
            return context.Alerts.OfType<WeeklyAlert>().First();
        }

        public IEnumerable<UserAlert> GetWeeklyUserAlets()
        {
            return context.UserAlerts.Where(u => u.Alert.AlertID.Equals(FindWeeklyAlert().AlertID)).ToList<UserAlert>();
        }

        public void RemoveUserAlert(UserAlert userAlert)
        {
            context.UserAlerts.Remove(userAlert);
            context.SaveChanges();
        }
    }
}
