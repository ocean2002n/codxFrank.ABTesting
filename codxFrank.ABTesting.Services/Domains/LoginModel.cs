using FluentValidation;

namespace codxFrank.ABTesting.Services.Domains
{
    public class LoginViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }

    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Username).NotNull().WithMessage("{PropertyName} 不可以為 null。");
            RuleFor(x => x.Password).NotNull().WithMessage("{PropertyName} 不可以為 null。");
        }
    }

}