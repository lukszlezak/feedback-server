using Gmtl.Feedback.Server.Models;
using Gmtl.Feedback.Server.Services;
using Gmtl.Feedback.Server.ViewModels;
using Gmtl.HandyLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http.Headers;

namespace Gmtl.Feedback.Server.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackAPIController : Controller
    {
        private readonly DomainService domainService;
        private readonly FeedbackService feedbackService;

        public FeedbackAPIController(DomainService domainService, FeedbackService feedbackService)
        {
            this.domainService = domainService;
            this.feedbackService = feedbackService;
        }

        [HttpPost("submit")]
        public IActionResult Submit([FromHeader(Name = "apiKey")]string apiKey, [FromBody] Models.Feedback feedback)
        {
            Domain domain = domainService.GetDomain(Request.Host.Host);

            if (domain != null && domainService.CheckApiKeyAndDomainPair(apiKey, domain.Id))
            {
                feedback.Id = Guid.NewGuid();
                feedback.DomainId = domain.Id;
                feedback.CreateDate = DateTime.Now;
                feedbackService.Add(feedback);

                return Content(OperationResult<bool>.Success(message: "Feedback registered!").AsJson, "application/json");
            }

            return Content(OperationResult<bool>.Error(message: "Could not register feedback").AsJson, "application/json");
        }
    }
}