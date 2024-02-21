using ASPNET_HHRR_Vacations.Models;
using ASPNET_HHRR_Vacations.Services.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

class Program
{
    static async Task Main()
    {
        var serviceProvider = ConfigureServices();

        AdminCreator.Employee employeeInitial = new();
        AdminCreator.UserCredential credentialInitial = new();
        bool isAdmin = ConsoleUserType();

        var employeeFinal = ConsoleNameReader(employeeInitial);

        var credentialFinal = ConsoleReader(credentialInitial);

        if (employeeFinal != null && credentialFinal != null)
        {
            await CreateAdmin(employeeFinal, credentialFinal, serviceProvider, isAdmin);
        }
    }
    static async Task CreateAdmin(AdminCreator.Employee employeeInput, AdminCreator.UserCredential credentialInput, ServiceProvider service, bool isAdmin)
    {
        try
        {
            var userService = service.GetRequiredService<IUserService>();
            UserCredential credential = new()
            {
                PasswordHash = credentialInput.Password,
                Email = credentialInput.Email,
                IsAdmin = isAdmin
            };
            Employee employee = new()
            {
                FirstName = employeeInput.FirstName,
                LastName = employeeInput.LastName
            };

            employee.UserCredential = credential;
            var result = await userService.CreateUser(employee);
            if (result.IsSuccess)
            {
                Console.WriteLine("User has been added successfully to the enterprise database");
            }
            else
            {
                Console.WriteLine(result.ErrorMessage);
            }
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine(ex);
        }
    }
    static ServiceProvider ConfigureServices()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("EnterpriseDB");

        return new ServiceCollection()
                .AddDbContext<EnterpriseContext>(options => options.UseSqlServer(connectionString))
                .AddScoped<IPasswordHashService, PasswordHashService>()
                .AddScoped<IUserService, UserService>()
                .BuildServiceProvider();
    }
    static bool ConsoleUserType()
    {
        string? input = null;
        bool isValid = false;
        Console.WriteLine("Create: ");
        Console.WriteLine("Press 1 - Admin");
        Console.WriteLine("Press 2 - Employee");
        do
        {

            input = Console.ReadLine();
            isValid = IsValidUserSelection(input);
            if (!isValid)
            {
                Console.WriteLine("Input must be 1 or 2.");
            }
        } while (!isValid);
        int selection = int.Parse(input);

        return selection == 1;
    }
    static AdminCreator.Employee ConsoleNameReader(AdminCreator.Employee employee)
    {
        bool isValid = false;
        string? firstName = null, lastName = null;
        Console.WriteLine("Insert the First name for the user.");
        do
        {
            firstName = Console.ReadLine();
            isValid = IsValidName(firstName);
            if (!isValid)
            {
                Console.WriteLine("Only letters are allowed for the first name, try again.");
            }
        } while (!isValid);
        Console.WriteLine("Insert the Last name for the user.");
        do
        {
            lastName = Console.ReadLine();
            isValid = IsValidName(lastName);
            if (!isValid)
            {
                Console.WriteLine("Only letters are allowed for the last name, try again.");
            }
        } while (!isValid);
        employee.FirstName = firstName;
        employee.LastName = lastName;
        return employee;
    }
    static AdminCreator.UserCredential ConsoleReader(AdminCreator.UserCredential userCredential)
    {
        bool isValid = false;
        string? email = null, password = null;
        Console.WriteLine("Insert a new email. Must follow: name.lastname, @enterprise.com is added automatically");
        do
        {
            email = Console.ReadLine();
            isValid = IsValidEmail(email);
            if (!isValid)
            {
                Console.WriteLine("Only lower letters and '.' symbol between are allowed");
            }

        } while (!isValid);

        isValid = false;
        Console.WriteLine("Insert a new password. Must have at least 8 characters min and 20 characters max.");
        do
        {
            password = Console.ReadLine();
            isValid = IsValidPassword(password);

        } while (!isValid);
        userCredential.Email = email;
        userCredential.Password = password;
        return userCredential;
    }
    static bool IsValidUserSelection(string input)
    {
        if (input == null)
            return false;
        string regularExpression = "^[1-2]$";
        return Regex.IsMatch(input, regularExpression);
    }
    static bool IsValidName(string name)
    {
        if (name == null)
            return false;
        string regularExpression = "^[a-zA-Z]+$";
        return Regex.IsMatch(name, regularExpression);
    }
    static bool IsValidPassword(string password)
     => password != null && (password.Length >= 8 && password.Length <= 20);
    static bool IsValidEmail(string email)
    {
        if (email == null)
            return false;
        string regularExpression = "^[a-z]+(\\.[a-z]+)?$";
        return Regex.IsMatch(email, regularExpression);
    }
}