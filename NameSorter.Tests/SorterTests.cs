using FluentAssertions;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace NameSorter.Tests
{
    public class SorterTests
    {
        [Fact]
        public void Should_Sort_Names_By_LastName_ThenBy_FirstNames()
        {
            IEnumerable<string> sortedNames = null;

            var getNames = new Func<IEnumerable<string>>(() => 
            {
                return new [] 
                {
                    "Janet Parsons",
                    "Vaughn Lewis",
                    "Adonis Julius Archer",
                    "Shelby Nathan Yoder",
                    "Marin Alvarez",
                    "London Lindsey",
                    "Beau Tristan Bentley",
                    "Leo Gardner",
                    "Hunter Uriah Mathew Clarke",
                    "Mikayla Lopez",
                    "Frankie Conner Ritter"
                };
            });

            var saveNames = new Action<IEnumerable<string>>(names =>
            {
                sortedNames = names;
            });

            var sorter = new Sorter(getNames, saveNames, sort: null);
            sorter.Sort();

            sortedNames.Should().NotBeNull();
            sortedNames.Should().Equal(new[]
                {
                    "Marin Alvarez",
                    "Adonis Julius Archer",
                    "Beau Tristan Bentley",
                    "Hunter Uriah Mathew Clarke",
                    "Leo Gardner",
                    "Vaughn Lewis",
                    "London Lindsey",
                    "Mikayla Lopez",
                    "Janet Parsons",
                    "Frankie Conner Ritter",
                    "Shelby Nathan Yoder",
                });

        }

        [Fact]
        public void Should_Sort_Similar_Names()
        {
            IEnumerable<string> sortedNames = null;

            var getNames = new Func<IEnumerable<string>>(() =>
            {
                return new[]
                {
                    "Hunter UriahA MathewB ClarkeB",
                    "Hunter UriahB MathewB ClarkeB",
                    "Hunter Uriah MathewB ClarkeB",
                    "Hunter Uriah MathewA ClarkeB",
                    "HunterB UriahB MathewB ClarkeB",
                    "HunterA UriahB MathewB ClarkeB",
                    "Hunter Uriah Mathew ClarkeB",
                    "Hunter Uriah Mathew ClarkeA",
                };
            });

            var saveNames = new Action<IEnumerable<string>>(names =>
            {
                sortedNames = names;
            });

            var sorter = new Sorter(getNames, saveNames, sort: null);
            sorter.Sort();

            sortedNames.Should().NotBeNull();
            sortedNames.Should().Equal(new[]
                {
                    "Hunter Uriah Mathew ClarkeA",
                    "Hunter Uriah Mathew ClarkeB",
                    "Hunter Uriah MathewA ClarkeB",
                    "Hunter Uriah MathewB ClarkeB",
                    "Hunter UriahA MathewB ClarkeB",
                    "Hunter UriahB MathewB ClarkeB",
                    "HunterA UriahB MathewB ClarkeB",
                    "HunterB UriahB MathewB ClarkeB",
                });

        }

        [Fact]
        public void Should_Ignore_Empty_Lines_When_Sorting()
        {
            IEnumerable<string> sortedNames = null;

            var getNames = new Func<IEnumerable<string>>(() =>
            {
                return new[]
                {
                    "Janet Parsons",
                    "Vaughn Lewis",
                    "",
                    "Adonis Julius Archer",
                    "Shelby Nathan Yoder",
                    "Marin Alvarez",
                    "London Lindsey",
                    "",
                    "Beau Tristan Bentley",
                    "Leo Gardner",
                    "Hunter Uriah Mathew Clarke",
                    "Mikayla Lopez",
                    "",
                    "Frankie Conner Ritter"
                };
            });

            var saveNames = new Action<IEnumerable<string>>(names =>
            {
                sortedNames = names;
            });

            var sorter = new Sorter(getNames, saveNames, sort: null);
            sorter.Sort();

            sortedNames.Should().NotBeNull();
            sortedNames.Should().Equal(new[]
                {
                    "Marin Alvarez",
                    "Adonis Julius Archer",
                    "Beau Tristan Bentley",
                    "Hunter Uriah Mathew Clarke",
                    "Leo Gardner",
                    "Vaughn Lewis",
                    "London Lindsey",
                    "Mikayla Lopez",
                    "Janet Parsons",
                    "Frankie Conner Ritter",
                    "Shelby Nathan Yoder",
                });

        }

        [Fact]
        public void Should_Read_And_Save_File()
        {
            var sorter = new Sorter(@"unsorted-names-list.txt", @"sorted-names-list.txt");
            sorter.Sort();

            var sortedNames = File.ReadAllLines(@"sorted-names-list.txt");
            sortedNames.Should().Equal(new[]
                {
                    "Marin Alvarez",
                    "Adonis Julius Archer",
                    "Beau Tristan Bentley",
                    "Hunter Uriah Mathew Clarke",
                    "Leo Gardner",
                    "Vaughn Lewis",
                    "London Lindsey",
                    "Mikayla Lopez",
                    "Janet Parsons",
                    "Frankie Conner Ritter",
                    "Shelby Nathan Yoder",
                });
        }
    }
}
