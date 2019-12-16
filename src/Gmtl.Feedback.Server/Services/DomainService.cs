using Gmtl.Feedback.Server.Data;
using Gmtl.Feedback.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gmtl.Feedback.Server.Services
{
    public class DomainService
    {
        private readonly FeedbackDbContext context;

        public DomainService(FeedbackDbContext context)
        {
            this.context = context;
        }

        public bool Add(Domain domain)
        {
            context.Add(domain);

            //We generate a default API key
            ApiKey key = new ApiKey()
            {
                Id = Guid.NewGuid(),
                DomainId = domain.Id,
                KeyValue = "APIKEY-" + Guid.NewGuid().ToString(),
                CreateDate = DateTime.Now,
                Status = Status.Valid
            };

            context.Add(key);
            context.SaveChanges();

            return true;
        }

        public bool Edit(Domain domain)
        {
            context.Update(domain);
            context.SaveChanges();

            return true;
        }

        public bool CheckApiKeyAndDomainPair(string keyValue, Guid domainId)
        {
            return context.ApiKeys.Any(i => i.KeyValue == keyValue && i.Status == Status.Valid && i.DomainId == domainId);
        }

        public List<Domain> GetList(Guid userId)
        {
            return context.Domains.Where(i => i.UserId == userId).OrderBy(i => i.CreateDate).ToList();
        }

        public Domain GetDomain(Guid id, Guid userId)
        {
            return context.Domains.FirstOrDefault(i => i.Id == id && i.UserId == userId);
        }

        public Domain GetDomain(string name)
        {
            return context.Domains.FirstOrDefault(i => i.Name == name);
        }

        public List<ApiKey> GetKeysForDomain(Guid id)
        {
            return context.ApiKeys.Where(i => i.DomainId == id).OrderBy(i => i.CreateDate).ToList();
        }
    }
}