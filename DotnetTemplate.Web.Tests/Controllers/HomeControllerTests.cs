namespace DotnetTemplate.Web.Tests.Controllers
{
    using DotnetTemplate.Web.Controllers;
    using DotnetTemplate.Web.ViewModels.Home;
    using FluentAssertions;
    using FluentAssertions.AspNetCore.Mvc;
    using NUnit.Framework;

    public class HomeControllerTests
    {
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private HomeController controller;

        [SetUp]
        public void SetUp()
        {
            controller = new HomeController();
        }

        [Test]
        public void First_page_action_should_return_view_result()
        {
            // When
            var result = controller.FirstPage();

            // Then
            var expectedModel = new FirstPageViewModel();
            result.Should().BeViewResult()
                .Model.Should().BeEquivalentTo(expectedModel);
        }

        [Test]
        public void Second_page_action_should_return_view_result()
        {
            // When
            var result = controller.SecondPage();

            // Then
            var expectedModel = new SecondPageViewModel();
            result.Should().BeViewResult()
                .Model.Should().BeEquivalentTo(expectedModel);
        }
    }
}
