namespace TapMarket.Test.Controller
{
    using Microsoft.AspNetCore.Mvc;
    using TapMarket.Controllers;
    using Xunit;

    public class ListingControllerTest
    {
        [Fact]
        public void CreatedListingShouldReturnView()
        {
            //Arrange
            var listingController = new ListingController(null, null, null);

            //Act
            var result = listingController.CreatedListing();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void AddShouldReturnRedirect()
        {
            //Arrange
            var listingController = new ListingController(null, null, null);

            //Act
            var result = listingController.Add();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ViewComponentResult>(result);
        }
    }
}
