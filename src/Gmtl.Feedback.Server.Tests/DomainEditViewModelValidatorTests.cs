using Gmtl.Feedback.Server.ViewModels;
using FluentValidation.TestHelper;
using Xunit;

namespace Gmtl.Feedback.Server.Tests
{
    public class DomainEditViewModelValidatorTests
    {
        DomainEditViewModelValidator validator;

        public DomainEditViewModelValidatorTests()
        {
            validator = new DomainEditViewModelValidator();
        }

        [Theory]
        [InlineData("domenapl")]
        [InlineData("domenapl.")]
        [InlineData(".domenapl")]
        [InlineData("x.x.x")]
        [InlineData("xx.xx.x")]
        [InlineData("")]
        [InlineData("domena.pl.50")]
        public void InvalidDomain(string invalidDomainName)
        {
            validator.ShouldHaveValidationErrorFor(x => x.Name, invalidDomainName);
        }

        [Theory]
        [InlineData("domena.pl")]
        [InlineData("domena.35.com")]
        [InlineData("53.25.pl")]
        [InlineData("name.anothername.domainname.com")]
        public void CorrectDomain(string validDomainName)
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Name, validDomainName);
        }
    }
}
