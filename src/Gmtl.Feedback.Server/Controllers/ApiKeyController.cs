using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gmtl.Feedback.Server.Models;
using Gmtl.Feedback.Server.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gmtl.Feedback.Server.Controllers
{
    [Authorize]
    public class ApiKeyController : Controller
    {
        private readonly ApiKeyService apiKeyService;

        public ApiKeyController (ApiKeyService apiKeyService)
        {
            this.apiKeyService = apiKeyService;
        }

        [HttpGet]
        public IActionResult ChangeStatus(Guid apiKeyId)
        {
            ApiKey apiKey = apiKeyService.GetApiKey(apiKeyId);
            if (apiKey == null)
            {
                return NotFound("Something goes wrong");
            }

            apiKey.Status = (apiKey.Status == Status.Valid) ? Status.Invalid : Status.Valid;
            apiKeyService.UpdateApiKey(apiKey);

            return RedirectToAction("Edit", "Domain", new { domainId = apiKey.DomainId });
        }

        [HttpGet]
        public IActionResult Regenerate(Guid apiKeyId)
        {
            ApiKey apiKey = apiKeyService.GetApiKey(apiKeyId);
            if (apiKey == null)
            {
                return NotFound("Something goes wrong");
            }

            apiKey.KeyValue = "APIKEY-" + Guid.NewGuid().ToString();
            apiKeyService.UpdateApiKey(apiKey);

            return RedirectToAction("Edit", "Domain", new { domainId = apiKey.DomainId });
        }

        [HttpGet]
        public IActionResult Add(Guid domainId)
        {
            ApiKey apiKey = new ApiKey()
            {
                Id = Guid.NewGuid(),
                DomainId = domainId,
                KeyValue = "APIKEY-" + Guid.NewGuid().ToString(),
                CreateDate = DateTime.Now,
                Status = Status.Valid
            };
            apiKeyService.AddApiKey(apiKey);

            return RedirectToAction("Edit", "Domain", new { domainId });
        }
    }
}