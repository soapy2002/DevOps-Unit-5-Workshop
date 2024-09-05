namespace DotnetTemplate.Web.Tests.ViewModels.Home
{
    using DotnetTemplate.Web.ViewModels.Home;
    using FluentAssertions;
    using NUnit.Framework;

    public class FirstPageViewModelTests
    {
        [Test]
        public void First_page_view_model_should_have_items()
        {
            // Given
            var expectedItems = new[]
            {
                "Item 1",
                "Item 2",
                "Item 3"
            };

            // When
            var firstPageViewModel = new FirstPageViewModel();

            // Then
            firstPageViewModel.FirstPageItems.Should().BeEquivalentTo(expectedItems);
        }
    }
}
