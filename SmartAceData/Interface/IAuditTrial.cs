using SmartAceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAceData.Interface
{
  public  interface IAuditTrial :IDisposable
    {
        void AddAuditTrail(AuditTrail auditTrail);
        AuditTrail FindAuditTrailByID(int ID);
        ICollection<AuditTrail> GetAllAuditTrails();
        ICollection<AuditTrail> GetAuditTrailByMember(string username);
        ICollection<AuditTrail> GetAudittrailByDateRange(DateTime startDate, DateTime enddate);
        ICollection<AuditTrail> GetAuditTrailByMemberAndDateRange(string username, DateTime? startDate, DateTime? endDate);
        AuditTrail GetAuditTrailByIUserId(string userId);
    }
}
