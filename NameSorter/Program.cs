﻿using System;
using System.Linq;

namespace NameSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            var (sourceFilePath, targetFilePath) = ParseArgs(args);

            try
            {
                var sorter = new Sorter(sourceFilePath, targetFilePath);
                sorter.Sort();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred. [message: {ex.Message}]");
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadLine();
        }

        static (string sourceFilePath, string targetFilePath) ParseArgs(string[] args)
        {
            var sourceFilePath = @"unsorted-names-list.txt";
            var targetFilePath = @"sorted-names-list.txt";

            if (args != null && args.Any()) 
            {
                if (args.Length >= 1)
                    sourceFilePath = args[0];
                if (args.Length >= 2)
                    targetFilePath = args[1];
            }

            Console.WriteLine($"Source File: {sourceFilePath}");
            Console.WriteLine($"Target File: {targetFilePath}");

            return (sourceFilePath, targetFilePath);
        }
    }
}
