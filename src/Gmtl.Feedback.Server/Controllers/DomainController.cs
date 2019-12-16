using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gmtl.Feedback.Server.Models;
using Gmtl.Feedback.Server.Services;
using Gmtl.Feedback.Server.ViewModels;
using Gmtl.HandyLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gmtl.Feedback.Server.Controllers
{
    [Authorize]
    public class DomainController : BaseUserController
    {
        private readonly DomainService service;

        public DomainController(DomainService service)
        {
            this.service = service;
        }

        public IActionResult List()
        {
            return View(new DomainListViewModel { Domains = service.GetList(UserId) });
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(DomainAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                service.Add(new Domain
                {
                    Id = Guid.NewGuid(),
                    UserId = UserId,
                    Name = model.Name,
                    Description = model.Description,
                    CreateDate = DateTime.Now
                });
                return RedirectToAction("List");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(Guid domainId)
        {
            Domain domain = service.GetDomain(domainId, UserId);
            if (domain == null)
            {
                return NotFound("Something goes wrong");
            }

            List<ApiKey> keysForDomain = service.GetKeysForDomain(domainId);

            DomainEditViewModel model = new DomainEditViewModel
            {
                Id = domain.Id,
                Name = domain.Name,
                Description = domain.Description,
                KeysForDomain = keysForDomain
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(DomainEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Domain domain = service.GetDomain(model.Id, UserId);
                if (domain == null)
                {
                    return NotFound("Something goes wrong");
                }

                domain.Name = model.Name;
                domain.Description = model.Description;
                service.Edit(domain);

                return RedirectToAction("List");
            }

            model.KeysForDomain = service.GetKeysForDomain(model.Id);

            return View(model);
        }
    }
}