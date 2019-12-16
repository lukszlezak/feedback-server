using Gmtl.Feedback.Server.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmtl.Feedback.Server.Services
{
    public class FeedbackService
    {
        private readonly FeedbackDbContext context;

        public FeedbackService(FeedbackDbContext context)
        {
            this.context = context;
        }

        public bool Add(Models.Feedback feedback)
        {
            context.Add(feedback);
            context.SaveChanges();

            return true;
        }

        public List<Models.Feedback> GetDashBoard(Guid userId)
        {
            List<Models.Feedback> result = context.Feedbacks.Where(f => f.Domain.UserId == userId).OrderByDescending(f => f.CreateDate).ToList();

            return result.GetRange(0, Math.Min(8, result.Count));
        }

        public List<Models.Feedback> GetList(Guid domainId)
        {
            return context.Feedbacks.Where(f => f.DomainId == domainId).OrderBy(f => f.CreateDate).ToList();
        }

        public List<Models.Feedback> Search(Guid domainId, string searchPattern)
        {
            return context.Feedbacks.Where(f => f.DomainId == domainId && f.Message.Contains(searchPattern, StringComparison.InvariantCultureIgnoreCase)).OrderBy(f => f.CreateDate).ToList();
        }

        public List<Models.Feedback> Search(string searchSignature, Guid domainId)
        {
            return context.Feedbacks.Where(f => f.DomainId == domainId && f.Signature.Contains(searchSignature, StringComparison.InvariantCultureIgnoreCase)).OrderBy(f => f.CreateDate).ToList();
        }

        public List<Models.Feedback> Search(Guid domainId, DateTime createDate)
        {
            return context.Feedbacks.Where(f => f.DomainId == domainId && f.CreateDate.Date == createDate).OrderBy(f => f.CreateDate).ToList();
        }
    }
}