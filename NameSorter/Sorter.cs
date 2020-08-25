using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NameSorter
{
    /// <summary>
    /// This class sorts a list of names
    /// </summary>
    public class Sorter
    {
        private readonly Func<IEnumerable<string>> _getUnsortedNamesList;
        private readonly Action<IEnumerable<string>> _saveSortedNames;
        private readonly Func<IEnumerable<string>, IEnumerable<string>> _sort;

        /// <summary>
        /// Creates an instance of Sorter that uses the default implementation for
        /// 1. Getting a list of unsorted names
        /// 2. The actual sorting
        /// 3. Saving / displaying the list of sorted names
        /// This constructor takes in 2 arguments. 
        /// </summary>
        /// <param name="sourceTextFile">The source file for the list of unsorted names</param>
        /// <param name="targetTextFile">The target file where the sorted names will be written</param>
        public Sorter(string sourceTextFile, string targetTextFile)
        {
            _getUnsortedNamesList = () => ReadNamesFromFile(sourceTextFile);
            _saveSortedNames = (sortedNames) => SaveNames(targetTextFile, sortedNames);
            _sort = Sort;
        }

        /// <summary>
        /// Creates an instance of Sorter where callers can supply their own implementation of 
        /// 1. Getting a list of unsorted names
        /// 2. The actual sorting
        /// 3. Saving / displaying the list of sorted names
        /// </summary>
        /// <param name="getUnsortedNames">A delegate that returns the list of unsorted names</param>
        /// <param name="saveSortedNames">A delegate that accepts a list of sorted names</param>
        /// <param name="sort">A delegate that does the actual sorting. This is null by default</param>
        public Sorter(Func<IEnumerable<string>> getUnsortedNames, Action<IEnumerable<string>> saveSortedNames, 
            Func<IEnumerable<string>, IEnumerable<string>> sort = null)
        {
            _getUnsortedNamesList = getUnsortedNames ?? throw new ArgumentNullException(nameof(getUnsortedNames));
            _saveSortedNames = saveSortedNames ?? throw new ArgumentNullException(nameof(saveSortedNames));
            _sort = sort ?? Sort;
        }

        public void Sort()
        {
            var unsortedNames = _getUnsortedNamesList();

            var sortedNames = _sort(unsortedNames);
            
            _saveSortedNames(sortedNames);
        }

        private IEnumerable<string> Sort(IEnumerable<string> unsortedNames)
        {
            const string _delimiter = " ";
            if (unsortedNames is null || !unsortedNames.Any())
            {
                Console.WriteLine($"The {nameof(unsortedNames)} enumerable is empty.");
                return new string[0];
            }

            return unsortedNames.Where(name => !string.IsNullOrEmpty(name))
                .Select(name =>
                {
                    var tokens = name.Split(new string[] { _delimiter }, StringSplitOptions.None);
                    var lastName = tokens.Last();

                    return new
                    {
                        LastName = lastName,
                        FullName = name
                    };
                })
                .OrderBy(i => i.LastName)
                .ThenBy(i => i.FullName)
                .Select(i => i.FullName)
                .ToList();
        }

        private IEnumerable<string> ReadNamesFromFile(string sourceTextFile)
        {
            if (string.IsNullOrEmpty(sourceTextFile) || !File.Exists(sourceTextFile))
            {
                Console.WriteLine($"The names text file does not exist. [Path: {sourceTextFile}]");
                return new string[0];
            }

            return File.ReadLines(sourceTextFile);
        }

        private void SaveNames(string targetFilePath, IEnumerable<string> sortedNames)
        {
            if (sortedNames is null || !sortedNames.Any())
            {
                return;
            }

            Console.WriteLine("Sorted Names:");
            Console.WriteLine(string.Join(Environment.NewLine, sortedNames));

            if (string.IsNullOrEmpty(targetFilePath))
            {
                Console.WriteLine($"The {nameof(targetFilePath)} is empty. Skippins save.");
                return;
            }

            File.WriteAllLines(targetFilePath, sortedNames);
        }
    }
}
