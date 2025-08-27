using Moq;
using RealEstate.Application.DTOs;
using RealEstate.Application.PropertiesService.Queries;
using RealEstate.Domain;
using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstate.Application.UnitTests.PropertiesService.Queries
{

    [TestFixture]
    public class GetPropertyDetailsHandlerTests
    {
        [Test]
        public async Task Handle_WhenInvokedWithCorrectData_ShouldReturnAndMapCorrectData()
        {
            //Arrange
            var mockRepository = new Mock<IPropertyRepository>();
            var propertyDetailsMock = new PropertyDetails()
            {
                Traces = [new() { IdProperty = "1", DateSale = "01/01/2020", IdPropertyTrace = "1", Name = "Trace1", Tax = 100, Value = 1000 }],
                Owner = new Owner { IdOwner = "1", Name = "Owner1", Address = "OwnerAddress1", Birthday = "02/08/1979", Photo = "Photo1" },
                Property = new Property { IdProperty = "1", Name = "Property1", Address = "Address1", Price = 1000, CodeInternal = "Code1", Year = 2020, IdOwner = "1", Image = "Image1" },
                Images = [
                new() { IdProperty = "1", Enabled = true, File = "File1", IdPropertyImage = "1" }
                ],
            };

            mockRepository.Setup(repo => repo.GetFilteredPropertyDetailsAsync(
                                It.IsAny<string>(),
                                It.IsAny<CancellationToken>()))
                .ReturnsAsync(propertyDetailsMock);

            //Act   
            var handler = new GetPropertyDetailsHandler(mockRepository.Object);
            var result = await handler.Handle(new GetPropertyDetailsQuery("1"), CancellationToken.None);

            //Assert
            mockRepository.Verify(repo => repo.GetFilteredPropertyDetailsAsync("1", It.IsAny<CancellationToken>()), Times.Once);
            Assert.IsNotNull(result);
            Assert.That(result.Property.IdProperty, Is.EqualTo("1"));
            Assert.That(result.Property.Name, Is.EqualTo(propertyDetailsMock.Property.Name));

        }

        [Test]
        public async Task Handle_ThrowsAnException_WhenRepositoryReturnsNull()
        {
            //Arrange
            var mockRepository = new Mock<IPropertyRepository>();
            mockRepository.Setup(repo => repo.GetFilteredPropertyDetailsAsync(
                                It.IsAny<string>(),
                                It.IsAny<CancellationToken>()))
                .ReturnsAsync((PropertyDetails?)null);

            var handler = new GetPropertyDetailsHandler(mockRepository.Object);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await handler.Handle(new GetPropertyDetailsQuery("1"), CancellationToken.None));
        }
    }
}
