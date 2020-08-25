# How to Build & Run

1. Open a shell to the root folder.
2. Run `dotnet build` to build the solution.
2. Navigate to the `NameSorter` folder `cd .\NameSorter\`
3. Run `dotnet run` to run the project
  * You can also pass arguments like `dotnet run -- "<source path>" "<target path>"`
4. Navigate to the `NameSorter.Tests` folder `cd .\NameSorter.Tests\`
5. Run `dotnet test` to run the tests.

# Assumptions

- The csv file does not have a header row
- Names are delimited by a space character
- The last text in the line is the "Last Name"
- There should be no "LineFeed" character within the name text

# Design Decisions

* The whole task can be divided into 3 responsibilties (that can possibly change)
  * Getting a list of unsorted names
  * Sorting the list
  * Saving / Displaying the list
* I've designed the `Sorter` class so that it's open for extension via a constructor that takes delegates for each responsibilty that can change.
* I though about using interfaces but delegates seems simpler.

