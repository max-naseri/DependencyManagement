using System;
using Xunit;

namespace DependencyManager.Tests
{
    public class DependencyManagementTests
    {
        [Fact]
        public void ResolveDependencies_ValidInput_ReturnsExpectedResult()
        {
            //Arrange
            var input = new string[,]
            {
                { "t-shirt", "dress shirt" },
                { "dress shirt", "pants" },
                { "dress shirt", "suit jacket" },
                { "tie", "suit jacket" },
                { "pants", "suit jacket" },
                { "belt", "suit jacket" },
                { "suit jacket", "overcoat" },
                { "dress shirt", "tie" },
                { "suit jacket", "sun glasses" },
                { "sun glasses", "overcoat" },
                { "left sock", "pants" },
                { "pants", "belt" },
                { "suit jacket", "left shoe" },
                { "suit jacket", "right shoe" },
                { "left shoe", "overcoat" },
                { "right sock", "pants" },
                { "right shoe", "overcoat" },
                { "t-shirt", "suit jacket" }
            };

            var expected = "left sock,right sock,t-shirt\ndress shirt\npants,tie\nbelt\nsuit jacket\nleft shoe,right shoe,sun glasses\novercoat";

            //Act
            var actual = DependencyManagement.ResolveDependencies(input);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ResolveDependencies_NullInput_ThrowArgumentNullException()
        {
            //Arrange
            string[,]? input = null;

            //Act & Assert
            Assert.Throws<ArgumentNullException>(() => DependencyManagement.ResolveDependencies(input!));
        }

        [Fact]
        public void ResolveDependencies_EmptyInput_ReturnsEmptyString()
        {
            // Arrange
            string[,] input = new string[0, 0];
            string expected = string.Empty;

            // Act
            var actual = DependencyManagement.ResolveDependencies(input);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ResolveDependencies_CyclicDependency_ThrowException()
        {
            // Arrange
            string[,] input = new string[,] { { "suit jacket", "dress shirt" }, { "dress shirt", "suit jacket" } };

            // Act & Assert
            Assert.Throws<Exception>(() => DependencyManagement.ResolveDependencies(input));
        }
    }
}