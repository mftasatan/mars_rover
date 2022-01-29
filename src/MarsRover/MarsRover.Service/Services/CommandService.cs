using MarsRover.Service.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Service.Services
{
    public class CommandService
    {
        private const string CommandsNullExceptionMessage = "Commands can not be null or empty";
        private const string RoverNullExceptionMessage = "Rover can not be null";
        private const string InvalidCommandExceptionMessage = "Commands should contain only L, M or R";

        public void ExecuteCommands(Rover rover, string commands)
        {
            if (string.IsNullOrEmpty(commands)) throw new ArgumentException(CommandsNullExceptionMessage);
            if (rover == null) throw new ArgumentException(RoverNullExceptionMessage);

            foreach (char command in commands)
            {
                var ecommand = ECommandHelper.GetCommand(command);

                if (ecommand == null) throw new ArgumentException(InvalidCommandExceptionMessage);

                ExecuteCommand(rover, ecommand.Value);

                if (rover.IsOutOfBounds()) return;
            }
        }

        public void ExecuteCommand(Rover rover, ECommand command)
        {
            switch (command)
            {
                case ECommand.Left:
                    RotateLeft(rover);
                    break;
                case ECommand.Right:
                    RotateRight(rover);
                    break;
                case ECommand.Move:
                    Move(rover);
                    break;
                default:
                    break;
            }
        }

        private void Move(Rover rover)
        {
            switch (rover.Direction)
            {
                case EDirection.North:
                    rover.CurrentPosition.Y++;
                    break;
                case EDirection.East:
                    rover.CurrentPosition.X++;
                    break;
                case EDirection.South:
                    rover.CurrentPosition.Y--;
                    break;
                case EDirection.West:
                    rover.CurrentPosition.X--;
                    break;
                default:
                    break;
            }
        }

        private void RotateRight(Rover rover)
        {
            Rotate(rover, 1);
        }

        private void RotateLeft(Rover rover)
        {
            Rotate(rover, -1);
        }

        private static void Rotate(Rover rover, int substractor)
        {
            int newDirection = ((int)rover.Direction + substractor) % 4;

            rover.Direction = (EDirection)(newDirection >= 0 ? newDirection : newDirection + 4);
        }
    }
}
