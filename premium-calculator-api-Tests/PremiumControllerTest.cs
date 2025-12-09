using NUnit.Framework;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;

using System.Collections.Generic;
using premium_calculator_api.Controllers;
using premium_calculator_api.Services;
using Microsoft.AspNetCore.Mvc;
using premium_calculator_api.Models;
namespace premium_calculator_api_Tests
{
    public class PremiumControllerTest
    {
        private PremiumController _controller;
        private Mock<IPremiumService> _serviceMock;

        [SetUp]
        public void Setup()
        {
            _serviceMock = new Mock<IPremiumService>();
            _controller = new PremiumController(_serviceMock.Object);
        }
        [Test]
        public void GetOccupations_ReturnsOkWithData()
        {
            // Arrange
             List < Occupation > occupations = new()
    {
        new Occupation { Id = 1, Name = "Cleaner", Rating = "Light Manual" },
        new Occupation { Id = 2, Name = "Doctor", Rating = "Professional" }
    };
            _serviceMock.Setup(s => s.GetOccupations()).Returns(occupations);

            // Act
            var result = _controller.GetOccupations() as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(occupations, result.Value);
            _serviceMock.Verify(s => s.GetOccupations(), Times.Once);
        }

        [Test]
        public void CalculatePremium_ReturnsCorrectPremium()
        {
            // Arrange
            var request = new PremiumRequest
            {
                AgeNextBirthday = 30,
                OccupationId = 1,
                DeathSumInsured = 500000
            };

            decimal expectedPremium = 1500;

            _serviceMock
                .Setup(s => s.CalculatePremium(It.IsAny<PremiumRequest>()))
                .Returns(expectedPremium);

            // Act
            var result = _controller.CalculatePremium(request) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);

            var response = result.Value as PremiumResponse;
            Assert.IsNotNull(response);
            Assert.AreEqual(expectedPremium, response.MonthlyPremium);

            _serviceMock.Verify(s => s.CalculatePremium(request), Times.Once);
        }

        // -------------------------------
        // Test: Null Request Should Fail (optional)
        // -------------------------------
        [Test]
        public void CalculatePremium_NullRequest_ReturnsBadRequest()
        {
            // Act
            var result = _controller.CalculatePremium(null) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
        }
    }
}