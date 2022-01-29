using MarsRover.Service.Model;
using MarsRover.Service.Services;
using System;
using System.Linq;
using Xunit;

namespace MarsRover.Tests
{
    public class InputServiceTests
    {
        private readonly InputService _inputService;

        public InputServiceTests()
        {
            _inputService = new InputService();
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldThrowExceptionForNullOrEmptyInputsForPlateu(string line)
        {
            Action act = () => _inputService.CreatePlateu(line);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Line can not be null", exception.Message);
        }

        [Theory]
        [InlineData("5")]
        [InlineData("test")]
        [InlineData("test 5")]
        public void ShouldThrowExceptionForParametersWhichAreNotTwoIntegersForPlateu(string line)
        {
            Action act = () => _inputService.CreatePlateu(line);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Line needs to contain two valid integers", exception.Message);
        }

        [Theory]
        [InlineData("5 -55")]
        [InlineData("-3 3")]
        [InlineData("-3 -5")]
        [InlineData("0 5")]
        [InlineData("5 0")]
        [InlineData("0 0")]
        public void ShouldThrowExceptionIfPlateusCoordinatesAreNotPositiveIntegers(string line)
        {
            Action act = () => _inputService.CreatePlateu(line);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Plateus coordinates should be positive integer values", exception.Message);
        }

        [Theory]
        [InlineData("5 5")]
        [InlineData("3 3")]
        [InlineData("3 5")]
        public void ShouldCreateThePlateuWithFirstLine(string line)
        {
            Plateu plateu = _inputService.CreatePlateu(line);

            Assert.Equal(plateu.UpperRightCoordinates.X, Convert.ToInt32(line.Split(" ").First()));
            Assert.Equal(plateu.UpperRightCoordinates.Y, Convert.ToInt32(line.Split(" ").Last()));
            Assert.Equal(0, plateu.LowerLeftCoordinates.X);
            Assert.Equal(0, plateu.LowerLeftCoordinates.Y);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldThrowExceptionForNullOrEmptyInputsForRover(string line)
        {
            Action act = () => _inputService.CreateRover(line, null);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Line can not be null", exception.Message);
        }

        [Theory]
        [InlineData("5")]
        [InlineData("test")]
        [InlineData("test 5")]
        [InlineData("test 5 e")]
        public void ShouldThrowExceptionForParametersWhichAreNotTwoIntegersForRover(string line)
        {
            Action act = () => _inputService.CreateRover(line, null);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Line needs to contain two valid integers and a valid Direction", exception.Message);
        }

        [Theory]
        [InlineData("5 5", "5 6 N")]
        public void ShouldThrowExceptionIfRoverIsOutOfBoundsWhenDeployed(string plateuLine, string roverLine)
        {
            var plateu = _inputService.CreatePlateu(plateuLine);

            Action act = () => _inputService.CreateRover(roverLine, plateu);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Rover coordinates needs to be inside of plateu", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCreatingARoverIfPlateuIsNull()
        {
            Action act = () => _inputService.CreateRover("1 2 N", null);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Plateu can not be null", exception.Message);
        }

        [Theory]
        [InlineData("1 2 N")]
        [InlineData("1 3 S")]
        public void ShouldCreateARoverForValidInputLine(string line)
        {
            Rover rover = _inputService.CreateRover(line, _inputService.CreatePlateu("5 5"));

            Assert.Equal(Convert.ToInt32(line.Split(" ").First()), rover.CurrentPosition.X);
            Assert.Equal(Convert.ToInt32(line.Split(" ")[1]), rover.CurrentPosition.Y);
            Assert.Equal(EDirectionHelper.GetDirection(line.Split(" ")[2]), rover.Direction);
        }
    }
}
