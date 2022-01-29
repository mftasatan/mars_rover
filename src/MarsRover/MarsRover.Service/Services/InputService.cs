using MarsRover.Service.Model;
using System;
using System.Linq;

namespace MarsRover.Service.Services
{
    public class InputService
    {
        private const string LineNullExceptionMessage = "Line can not be null";
        private const string LineNeedsToContainTwoIntegersMessage = "Line needs to contain two valid integers";
        private const string LineNeedsToContainTwoIntegersAndValidDirectionMessage = "Line needs to contain two valid integers and a valid Direction";
        private const string RoverCoordinatesNeedsToBeInsideOfPlateuMessage = "Rover coordinates needs to be inside of plateu";
        private const string PlateuCannotBeNullMessage = "Plateu can not be null";
        private const string PlateuCoordinatesShouldBePositiveMessage = "Plateus coordinates should be positive integer values";

        public Plateu CreatePlateu(string line)
        {
            if (string.IsNullOrEmpty(line)) throw new ArgumentException(LineNullExceptionMessage);
            if (line.Split(" ").Length != 2) throw new ArgumentException(LineNeedsToContainTwoIntegersMessage);

            int upperX;
            int upperY;

            if(!int.TryParse(line.Split(" ").First(), out upperX))
                throw new ArgumentException(LineNeedsToContainTwoIntegersMessage);

            if (!int.TryParse(line.Split(" ")[1], out upperY))
                throw new ArgumentException(LineNeedsToContainTwoIntegersMessage);

            if (upperX <= 0 || upperY <= 0)
                throw new ArgumentException(PlateuCoordinatesShouldBePositiveMessage);

            return new Plateu()
            {
                LowerLeftCoordinates = new Coordinates(0, 0),
                UpperRightCoordinates = new Coordinates(upperX, upperY)
            };
        }

        public Rover CreateRover(string line, Plateu plateu)
        {
            if (string.IsNullOrEmpty(line)) throw new ArgumentException(LineNullExceptionMessage);
            if (line.Split(" ").Length != 3) throw new ArgumentException(LineNeedsToContainTwoIntegersAndValidDirectionMessage);

            int currentX;
            int currentY;
            EDirection? direction = EDirectionHelper.GetDirection(line.Split(" ")[2]);

            if (!int.TryParse(line.Split(" ").First(), out currentX))
                throw new ArgumentException(LineNeedsToContainTwoIntegersAndValidDirectionMessage);

            if (!int.TryParse(line.Split(" ")[1], out currentY))
                throw new ArgumentException(LineNeedsToContainTwoIntegersAndValidDirectionMessage);

            if(!direction.HasValue)
            {
                throw new ArgumentException(LineNeedsToContainTwoIntegersAndValidDirectionMessage);
            }

            if (plateu == null) throw new ArgumentException(PlateuCannotBeNullMessage);

            Rover rover = new Rover()
            {
                CurrentPosition = new Coordinates(currentX, currentY),
                Direction = direction.Value,
                Plateu = plateu
            };

            if (rover.IsOutOfBounds()) throw new ArgumentException(RoverCoordinatesNeedsToBeInsideOfPlateuMessage);

            return rover;
        }
    }
}
