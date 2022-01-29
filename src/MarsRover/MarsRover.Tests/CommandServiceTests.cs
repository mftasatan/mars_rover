using MarsRover.Service.Model;
using MarsRover.Service.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MarsRover.Tests
{
    public class CommandServiceTests
    {
        private readonly CommandService _commandService = new CommandService();
        private readonly InputService _inputService = new InputService();

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void ShouldThrowExceptionWhenCommandsAreNull(string commands)
        {
            Action act = () => _commandService.ExecuteCommands(null, commands);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Commands can not be null or empty", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionIfRoverIsNull()
        {
            Action act = () => _commandService.ExecuteCommands(null , "LM");

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Rover can not be null", exception.Message);
        }

        [Theory]
        [InlineData("LdMD")]
        [InlineData("MMTMMTT")]
        public void ShouldThrowExceptionIfCommandsContainsInvalidCommands(string commands)
        {
            Rover rover = new Rover()
            {
                CurrentPosition = new Coordinates(0, 0),
                Direction = EDirection.North,
                Plateu = new Plateu { LowerLeftCoordinates = new Coordinates(0,0), UpperRightCoordinates = new Coordinates(5,5) }
            };

            Action act = () => _commandService.ExecuteCommands(rover, commands);

            ArgumentException exception = Assert.Throws<ArgumentException>(act);

            Assert.Equal("Commands should contain only L, M or R", exception.Message);
        }

        [Theory]
        [InlineData('L', "5 5", "1 2 N", "1 2 W")]
        [InlineData('L', "5 5", "1 2 W", "1 2 S")]
        [InlineData('M', "5 5", "1 2 N", "1 3 N")]
        [InlineData('R', "5 5", "1 2 N", "1 2 E")]
        [InlineData('R', "5 5", "1 2 W", "1 2 N")]
        [InlineData('M', "5 5", "5 5 N", "Out of Bounds!")]
        public void ShouldBeAtRightPositionForACommand(char command, string plateuLine, string roverLine, string expectedOutput)
        {
            var plateu = _inputService.CreatePlateu(plateuLine);
            var rover = _inputService.CreateRover(roverLine, plateu);

            _commandService.ExecuteCommand(rover, ECommandHelper.GetCommand(command).Value);

            Assert.Equal(expectedOutput, rover.GetOutput());
        }

        [Theory]
        [InlineData("5 5", "1 2 N", "LMLMLMLMM", "1 3 N")]
        [InlineData("5 5", "3 3 E", "MMRMMRMRRM", "5 1 E")]
        public void ShouldProduceValidOutputsForValidCommands(string plateuLine, string roverLine, string commands, string expectedOutput)
        {
            var plateu = _inputService.CreatePlateu(plateuLine);
            var rover = _inputService.CreateRover(roverLine, plateu);

            _commandService.ExecuteCommands(rover, commands);

            Assert.Equal(expectedOutput, rover.GetOutput());
        }
    }
}
