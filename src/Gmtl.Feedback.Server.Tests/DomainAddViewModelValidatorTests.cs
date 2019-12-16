using FluentValidation.TestHelper;
using Gmtl.Feedback.Server.ViewModels;
using Xunit;

namespace Gmtl.Feedback.Server.Tests
{
    public class DomainAddViewModelValidatorTests
    {
        DomainAddViewModelValidator validator;

        public DomainAddViewModelValidatorTests()
        {
            validator = new DomainAddViewModelValidator();
        }

        [Theory]
        [InlineData("o2.pl")]
        [InlineData("onet.pl")]
        [InlineData("google.com")]
        public void CorrectDomainShouldNotTriggerValidationError(string correctDomain)
        {
            //Act
            validator.ShouldNotHaveValidationErrorFor(d => d.Name, correctDomain);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Http:/dsad")]
        [InlineData("test")]
        [InlineData("123.123")]
        [InlineData("https://123.123")]
        public void InvalidDomainShouldTriggerValidationError(string invalidDomain)
        { 
            //Act    
            var result = validator.ShouldHaveValidationErrorFor(d => d.Name, invalidDomain);

            //Assert
            Assert.NotEmpty(result);
        }
    }
}
