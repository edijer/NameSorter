# How to Build & Run



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

