using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gmtl.Feedback.Server.Extensions;
using Gmtl.Feedback.Server.Models;
using Gmtl.Feedback.Server.Services;
using Gmtl.Feedback.Server.ViewModels;
using Gmtl.HandyLib;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gmtl.Feedback.Server.Controllers
{
    public abstract class BaseUserController : Controller
    {
        private Guid _userId;

        public Guid UserId
        {
            get
            {
                if (_userId == Guid.Empty)
                {
                    _userId = HttpContext.Session.Get<Guid>(IdentityCustomKeys.Id);
                    if (_userId == Guid.Empty)
                    {
                        throw new Exception();
                    }
                }

                return _userId;
            }
        }
    }
}