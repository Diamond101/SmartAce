using SmartAceData.Interface;
using SmartAceData.Repository;
using SmartAceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SmartAceLogic
{
   public class AuditTrialLogic
    {
        private readonly IAuditTrial _auditTrailRepository = new AuditTrialRepository();

        public ICollection<AuditTrail> GetAllAuditTrails()
        {
            return _auditTrailRepository.GetAllAuditTrails();
        }

        public AuditTrail FindAuditTrailById(int ID)
        {
            return _auditTrailRepository.FindAuditTrailByID(ID);
        }

        public ICollection<AuditTrail> GetAuditTrailBySearchCriteria(string username, DateTime? startDate, DateTime? endDate)
        {
            startDate = startDate.HasValue ? Convert.ToDateTime(startDate.Value.ToShortDateString() + " 00:00:00.000") : startDate;
            endDate = endDate.HasValue ? Convert.ToDateTime(endDate.Value.ToShortDateString() + " 23:59:59.000") : endDate;

            if (!string.IsNullOrWhiteSpace(username))
            {
                return _auditTrailRepository.GetAuditTrailByMember(username);
            }
            if (!string.IsNullOrWhiteSpace(username) && startDate.HasValue && endDate.HasValue)
            {
                return _auditTrailRepository.GetAuditTrailByMemberAndDateRange(username, startDate, endDate);
            }
            if (string.IsNullOrWhiteSpace(username) && startDate.HasValue && endDate.HasValue)
            {
                return _auditTrailRepository.GetAudittrailByDateRange(startDate.Value, endDate.Value);
            }
            return _auditTrailRepository.GetAllAuditTrails();
        }

        public void SaveAuditTrail(string Event)
        {
            UserSession userSession = (UserSession)HttpContext.Current.Session["UserSession"] ?? new UserSession() { Email = (string)HttpContext.Current.Session["FailedUser"] };

            try
            {
                AuditTrail auditTrail = new AuditTrail()
                {
                    UserID = userSession.Email,
                    MachineName = Environment.MachineName.ToUpper(),
                    DateTime = DateTime.Now,
                    Date_Time_UTC = DateTime.UtcNow,
                    Event = Event,
                    Module = HttpContext.Current.Request.Url.AbsoluteUri
                };

                _auditTrailRepository.AddAuditTrail(auditTrail);
            }
            catch (Exception e)
            {
                //ErrorLog.LogException(e);
            }
        }

        public AuditTrail GetAuditTrailByIUserId(string userId)
        {
            return _auditTrailRepository.GetAuditTrailByIUserId(userId);
        }
    }
}
