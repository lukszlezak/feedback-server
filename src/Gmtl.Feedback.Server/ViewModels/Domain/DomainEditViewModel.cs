using FluentValidation;
using Gmtl.Feedback.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gmtl.Feedback.Server.ViewModels
{
    public class DomainEditViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ApiKey> KeysForDomain { get; set; }
    }

    public class DomainEditViewModelValidator : AbstractValidator<DomainEditViewModel>
    {
        public DomainEditViewModelValidator()
        {
            //TODO validate http prefix and domain existance
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Domain name must be set")
                .NotNull().WithMessage("Domain name must be set")
                .Matches("^([a-z0-9]([a-z0-9_-]{0,61}[a-z0-9])?\\.)+[a-z]{2,6}$").WithMessage("Domain name must be correct");
        }
    }
}
