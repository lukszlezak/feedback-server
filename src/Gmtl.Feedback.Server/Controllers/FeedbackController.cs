using Gmtl.Feedback.Server.Models;
using Gmtl.Feedback.Server.Services;
using Gmtl.Feedback.Server.ViewModels;
using Gmtl.HandyLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gmtl.Feedback.Server.Controllers
{
    [Authorize]
    public class FeedbackController : BaseUserController
    {
        private readonly FeedbackService feedbackService;

        public FeedbackController(FeedbackService feedbackService)
        {
            this.feedbackService = feedbackService;
        }

        public IActionResult DashBoard()
        {
            return View(new DashBoardViewModel { Feedbacks = feedbackService.GetDashBoard(UserId) });
        }

        [HttpGet]
        public IActionResult List(Guid domainId, string domainName)
        {
            FeedbackListViewModel model = new FeedbackListViewModel
            {
                Feedbacks = feedbackService.GetList(domainId),
                DomainId = domainId,
                DomainName = domainName
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult SearchMessages(FeedbackListViewModel model)
        {
            if (string.IsNullOrEmpty(model.SearchPattern))
            {
                return RedirectToAction("List", new { model.DomainId, model.DomainName });
            }

            model.Feedbacks = feedbackService.Search(model.DomainId, model.SearchPattern);
            if (model.Feedbacks.Count == 0)
            {
                model.ErrorMessage = "No feedbacks!";
            }

            return View("List", model);
        }

        [HttpPost]
        public IActionResult SearchSignatures(FeedbackListViewModel model)
        {
            if (string.IsNullOrEmpty(model.SearchSignature))
            {
                return RedirectToAction("List", new { model.DomainId, model.DomainName });
            }

            model.Feedbacks = feedbackService.Search(model.SearchSignature, model.DomainId);
            if (model.Feedbacks.Count == 0)
            {
                model.ErrorMessage = "No feedbacks!";
            }

            return View("List", model);
        }

        [HttpPost]
        public IActionResult SearchDates(FeedbackListViewModel model)
        {
            model.Feedbacks = feedbackService.Search(model.DomainId, model.CreateDate);
            if (model.Feedbacks.Count == 0)
            {
                model.Feedbacks.Add(new Models.Feedback { Message = "No feedbacks!" });
            }

            return View("List", model);
        }

        [HttpGet]
        public IActionResult ExportCSV(Guid domainId, string domainName)
        {
            var feedbacks = feedbackService
                .GetList(domainId)
                .Select(i => String.Join(',', i.Message, i.Signature, i.CreateDate.ToString("dd-MM-yyyy")));

            var data = new List<string>() { "Message, Signature, Create date" };
            data.AddRange(feedbacks);

            byte[] buffer = Encoding.ASCII.GetBytes(String.Join(Environment.NewLine, data.ToArray()));

            return File(buffer, "text/csv", "Feedbacks of " + domainName + ".csv");
        }
    }
}