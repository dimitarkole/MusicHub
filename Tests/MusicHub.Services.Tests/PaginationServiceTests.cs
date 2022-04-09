using MusicHub.Services.Interfaces;
using System;
using Xunit;

namespace MusicHub.Services.Tests
{
    public class PaginationServiceTests
    {
        private const string InvalidElementsPerPageErrorMessage = "Elements per page cannot be negative or zero number.";
        private const string InvalidElementsCountErrorMessage = "Elements count cannot be negative number.";

        [Theory]
        [InlineData(100, 12)]
        [InlineData(5, 3)]
        public void CalculatePagesCount_WhitSameBussinesLogic(int elementsCount, int elementsPerPage)
        {
            var service = new PaginationService();
            var expected = this.CalculatePagesCount(elementsCount, elementsPerPage);
            var actual = service.CalculatePagesCount(elementsCount, elementsPerPage);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(-5, 5)]
        public void CalculatePagesCount_ElementsCountAreLessThanZiro_ShouldHasArgumentException(int elementsCount, int elementsPerPage)
        {
            var service = new PaginationService();
            var ex = Assert.Throws<ArgumentException>(() => service.CalculatePagesCount(elementsCount, elementsPerPage));
            Assert.Equal(InvalidElementsCountErrorMessage, ex.Message);
        }

        [Theory]
        [InlineData(25, -5)]
        public void CalculatePagesCount_ElementsPerPageAreZiro_ShouldHasArgumentException(int elementsCount, int elementsPerPage)
        {
            var service = new PaginationService();
            var ex = Assert.Throws<ArgumentException>(() => service.CalculatePagesCount(elementsCount, elementsPerPage));
            Assert.Equal(InvalidElementsPerPageErrorMessage, ex.Message);
        }

        public int CalculatePagesCount(int elementsCount, int elementsPerPage)
        {
            if (elementsPerPage <= 0)
            {
                throw new ArgumentException(InvalidElementsPerPageErrorMessage);
            }

            if (elementsCount < 0)
            {
                throw new ArgumentException(InvalidElementsCountErrorMessage);
            }

            return (int)Math.Ceiling((double)elementsCount / elementsPerPage);
        }

    }
}