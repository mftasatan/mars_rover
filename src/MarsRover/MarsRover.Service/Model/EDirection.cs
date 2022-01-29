using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Service.Model
{
    public enum EDirection
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    }

    public static class EDirectionHelper
    {
        public static EDirection? GetDirection(string direction)
        {
            if (string.IsNullOrEmpty(direction) || direction.Length > 1) return null;

            return GetDirection(direction[0]);
        }

        public static EDirection? GetDirection(char direction)
        {
            switch (direction)
            {
                case 'N':
                    return EDirection.North;
                case 'S':
                    return EDirection.South;
                case 'W':
                    return EDirection.West;
                case 'E':
                    return EDirection.East;
                default:
                    return null;
            }
        }

        public static char GetDirectionCharValue(this EDirection direction)
        {
            return direction.ToString()[0];
        }
    }
}
