namespace DotnetTemplate.Web.Tests.ViewModels.Home
{
    using DotnetTemplate.Web.ViewModels.Home;
    using FluentAssertions;
    using NUnit.Framework;

    public class SecondPageViewModelTests
    {
        [Test]
        public void Second_page_view_model_should_have_items()
        {
            // Given
            var expectedItems = new[]
            {
                "Second Page Item 1",
                "Second Page Item 2",
                "Second Page Item 3"
            };

            // When
            var firstPageViewModel = new SecondPageViewModel();

            // Then
            firstPageViewModel.SecondPageItems.Should().BeEquivalentTo(expectedItems);
        }
    }
}
