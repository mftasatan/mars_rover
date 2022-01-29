using MarsRover.Service.Services;
using System;
using System.Collections.Generic;
using System.IO;

namespace MarsRover.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            if (lines.Length == 0) return;

            var inputService = new InputService();
            var commandService = new CommandService();

            var plateu = inputService.CreatePlateu(lines[0]);

            List<string> result = new List<string>();

            for (int i = 1; i < lines.Length; i+=2)
            {
                var rover = inputService.CreateRover(lines[i], plateu);

                commandService.ExecuteCommands(rover, lines[i + 1]);

                result.Add(rover.GetOutput());
            }

            File.WriteAllLines("output.txt", result);
        }
    }
}
