using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Service.Model
{
    public enum ECommand
    {
        Left,
        Right,
        Move
    }

    public static class ECommandHelper
    {
        public static ECommand? GetCommand(string command)
        {
            if (string.IsNullOrEmpty(command) || command.Length > 1) return null;

            return GetCommand(command[0]);
        }

        public static ECommand? GetCommand(char command)
        {
            switch (command)
            {
                case 'L':
                    return ECommand.Left;
                case 'R':
                    return ECommand.Right;
                case 'M':
                    return ECommand.Move;
                default:
                    return null;
            }
        }
    }
}
