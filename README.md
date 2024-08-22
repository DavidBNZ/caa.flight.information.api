# CAA Flight Information API

The Entry point of this program is `Program.cs` in the caa.flight.information.api project
The Tests are in the project caa.flight.information.api.tests

To run this in Visual Studio 2022, Open the repository, Ensure that Nuget has pulled the required packages, and Build/Execute the main program using Ctrl-f5.

You will require Dot Net 8 Core.


## Notes 
There are more tests that I would like to have written, but am less familiar with Entity Framework.
I found a set of disagreeing information about how to test it, most leaning towards using an in memory database.
In using Moq.EntityFrameworkCore, I expected this to be handled as this is what it's selling point was, but I found in my testing that this did not update on POSTs.

If I were to continue, I would intend to host my own in memory database and remove the use of Moq in the testing.
