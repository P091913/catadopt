// client did not touch our infrastructure
// reference infrastructure from client
// later we'll remove this reference and use the .dll build files

// Setup a host builder, this is going to contain all of our
// services and tools (configs)
using Microsoft.Extensions.Hosting;
using CatAdoption.Application;
using CatAdoption.Infrastructure;
using CatAdoption.Infrastructure.Data;
using CatAdoption.Application.Services;
using Microsoft.Extensions.DependencyInjection;


var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        services.AddApplication();
        services.AddInfrastructure("Data Source=cats.db"); // add dependency injection to catadoption.infrastructure
    }).Build();

using (var scope = host.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var dbContext = services.GetRequiredService<AdoptionDbContext>();
        dbContext.Database.EnsureCreated();

        var appService = services.GetRequiredService<AdoptionService>();

        bool keepRunning = true;

        while (keepRunning)
        {
            Console.WriteLine("Cat Adoption Main Screen");
            Console.WriteLine("1. Add Owner");
            Console.WriteLine("2. Adopt Cat");
            Console.WriteLine("3. List All Owners");
            Console.WriteLine("4. View All Cats");
            Console.WriteLine("5. Exit");
            Console.Write("Select an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Enter Owner Name: ");
                    var name = Console.ReadLine();
                    Console.WriteLine("What is the owners age: ");
                    // validation logic here or for production, your application should be handling validation
                    if (int.TryParse(Console.ReadLine(), out var age))
                    {
                        await appService.CreateOwnerAsync(name!, age);
                    }
                    break;
                case "2":
                    Console.WriteLine("Enter cat name: ");
                    var catName = Console.ReadLine();
                    Console.WriteLine("Enter owner id");
                    if (int.TryParse(Console.ReadLine(), out var ownerId))
                    {
                        await appService.AddingCatAsync(catName!, DateTime.Now, ownerId);
                    }

                    break;
                case "3":
                    await appService.ListAllOwnerAsync();
                    break;
                case "4":
                    await appService.ListAllCatAsync();
                    break;
                case "5":
                    keepRunning = false;
                    break;
            }
        }
    }
    catch (Exception except)
    {
        Console.WriteLine($"Error message is: {except.Message}");
    }
}

