using FluentValidation;
using Gmtl.Feedback.Server.Services;

namespace Gmtl.Feedback.Server.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        /// <summary>
        /// Email
        /// </summary>
        public string Login { get; set; }
        public string Password { get; set; }
        public string Password2 { get; set; }
        public bool Regulations { get; set; }
    }

    public class RegisterViewModelValidator : AbstractValidator<RegisterViewModel>
    {
        private readonly UserService userService;

        public RegisterViewModelValidator(UserService userService)
        {
            this.userService = userService;

            RuleFor(x => x.Password)
                .Equal(x => x.Password2).WithMessage("Passwords must match")
                .MinimumLength(8).WithMessage("Password must be minimum 8 charasters");

            RuleFor(x => x.Login)
                .EmailAddress().WithMessage("Login must be an e-mail")
                .Must(IsLoginFree).WithMessage("This login exist");

            RuleFor(x => x.Regulations)
                .Must(x => x.Equals(true)).WithMessage("You must accept regulations");
        }

        private bool IsLoginFree(string login)
        {
            return userService.IsLoginFree(login);
        }
    }
}