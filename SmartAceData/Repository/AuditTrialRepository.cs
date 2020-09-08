using SmartAceData.Interface;
using SmartAceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartAceData.Repository
{
  public  class AuditTrialRepository: IAuditTrial
    {
        private readonly SmartAceDBContext _db = new SmartAceDBContext();

        private bool disposed = false;

        protected virtual void Dispose(bool Disposing)
        {
            if (!this.disposed)
            {
                if (Disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void Save()
        {
            _db.SaveChanges();
        }
        public void AddAuditTrail(AuditTrail auditTrail)
        {
            _db.AuditTrail.Add(auditTrail);
            Save();
        }
        public AuditTrail FindAuditTrailByID(int ID)
        {
            return _db.AuditTrail.Find(ID);
        }

        public ICollection<AuditTrail> GetAllAuditTrails()
        {
            return _db.AuditTrail
                 .OrderByDescending(at => at.DateTime)
                 .ToList();
        }

        public ICollection<AuditTrail> GetAudittrailByDateRange(DateTime startDate, DateTime endDate)
        {
            return _db.AuditTrail
                .Where(at => at.DateTime >= startDate && at.DateTime <= endDate)
                .OrderByDescending(at => at.DateTime)
                .ToList();
        }


        public AuditTrail GetAuditTrailByIUserId(string userId)
        {
            return _db.AuditTrail
                .FirstOrDefault(at => at.UserID == userId);
        }

        public ICollection<AuditTrail> GetAuditTrailByMember(string username)
        {
            return _db.AuditTrail
               .Where(at => at.UserID.ToLower().Contains(username))
               .OrderByDescending(at => at.DateTime)
               .ToList();
        }

        public ICollection<AuditTrail> GetAuditTrailByMemberAndDateRange(string username, DateTime? startDate, DateTime? endDate)
        {
            return _db.AuditTrail
                .Where(at => at.UserID == username)
                .Where(at => at.DateTime >= startDate && at.DateTime <= endDate)
                .OrderByDescending(at => at.DateTime)
                .ToList();
        }
    }
}

    
