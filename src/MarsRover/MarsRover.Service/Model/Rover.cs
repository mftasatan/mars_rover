using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRover.Service.Model
{
    public class Rover
    {
        public Coordinates CurrentPosition { get; set; }
        public EDirection Direction { get; set; }
        public Plateu Plateu { get; set; }

        public bool IsOutOfBounds()
        {
            return !(CurrentPosition.X >= Plateu.LowerLeftCoordinates.X && CurrentPosition.X <= Plateu.UpperRightCoordinates.X
                && CurrentPosition.Y >= Plateu.LowerLeftCoordinates.Y && CurrentPosition.Y <= Plateu.UpperRightCoordinates.Y);
        }

        public string GetOutput()
        {
            if (IsOutOfBounds()) return "Out of Bounds!";

            return $"{CurrentPosition.X} {CurrentPosition.Y} {Direction.GetDirectionCharValue().ToString()}";
        }
    }
}
