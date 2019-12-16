using Gmtl.Feedback.Server.Data;
using Gmtl.Feedback.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmtl.Feedback.Server.Services
{
    public class ApiKeyService
    {
        private readonly FeedbackDbContext context;

        public ApiKeyService(FeedbackDbContext context)
        {
            this.context = context;
        }

        public ApiKey GetApiKey(Guid id)
        {
            return context.ApiKeys.FirstOrDefault(i => i.Id == id);
        }

        public bool UpdateApiKey(ApiKey apiKey)
        {
            context.Update(apiKey);
            context.SaveChanges();

            return true;
        }

        public bool AddApiKey(ApiKey apiKey)
        {
            context.Add(apiKey);
            context.SaveChanges();

            return true;
        }
    }
}