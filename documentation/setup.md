1. Download  Visual Studio Code: It has great support for C# and .NET projects.
2. Install the .NET SDK: Install the .NET SDK (if not already installed) from Microsoft's official site. 
3. Clone the repository: Clone the repository to your local machine.
4. Open the project in Visual Studio Code and open its terminal (Ctrl + `).
5. `cd` into the `experiment` subdirectory of the cloned repository.
6. Run `dotnet run` to start the project.
7. If this fails, terminate the process (Ctrl + C) and install some additional dependencies using the terminal:
8. Run `dotnet add package SixLabors.ImageSharp --version 3.1.0` to install the ImageSharp library.
9. Run `dotnet tool install --global dotnet-ef` to install the Entity Framework Core CLI tool.
10. Run `dotnet ef database update` to apply the migrations and create the database.
11. Run `dotnet run` again to start the project.