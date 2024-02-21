using ASPNET_HHRR_Vacations.Helpers;
using ASPNET_HHRR_Vacations.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_HHRR_Vacations.Services.Authentication
{
    public class UserService : IUserService
    {
        private readonly EnterpriseContext _enterpriseContext;
        private readonly IPasswordHashService _passwordHashService;
        public UserService(EnterpriseContext enterpriseContext, IPasswordHashService passwordHashService)
        {
            _enterpriseContext = enterpriseContext;
            _passwordHashService = passwordHashService;
        }
        private async Task<UserResult> CreateEmployee(Employee employee)
        {
            UserResult authResult = new();
            try
            {
                string? firstName = employee.FirstName.Capitalize();
                string? lastName = employee.LastName.Capitalize();

                Employee user = new Employee()
                {
                    FirstName = firstName,
                    LastName = lastName,
                };
                await _enterpriseContext.Employees.AddAsync(user);
                await _enterpriseContext.SaveChangesAsync();
                authResult.IsSuccess = true;
                authResult.ObjectResult = user;
            }
            catch (DbUpdateException ex)
            {
                authResult.ErrorMessage = "An error ocurred while trying to create the credentials, try again.";
            }
            catch (Exception ex)
            {
                authResult.ErrorMessage = "An unkown error ocurred while trying to create the credentials, try again.";
            }
            return authResult;
        }
        private async Task<AuthResult> CreateCredentials(UserCredential credentials)
        {
            AuthResult userResult = new();
            try
            {
                string? email = GetFormattedEmail(credentials);
                credentials.Email = email;
                string passwordCreated = credentials.PasswordHash;
                string passwordHashed = _passwordHashService.HashPassword(credentials, passwordCreated);
                credentials.PasswordHash = passwordHashed;
                await _enterpriseContext.UserCredentials.AddAsync(credentials);
                await _enterpriseContext.SaveChangesAsync();
                userResult.IsSuccess = true;
                userResult.ObjectResult = credentials;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine(ex);
                userResult.ErrorMessage = "An error ocurred while trying to create the credentials, try again.";
            }
            catch (Exception ex)
            {
                userResult.ErrorMessage = "An unkown error ocurred while trying to create the credentials, try again.";
            }
            return userResult;
        }

        public async Task<AuthResult> CreateUser(Employee employee)
        {
            AuthResult authResult = new();
            try
            {
                string? email = GetFormattedEmail(employee.UserCredential);
                bool emailExists = await UserCrentialsExistsByEmail(email);

                if (emailExists)
                {
                    authResult.ErrorMessage = $"The current email {email} exists in the database.";
                    authResult.IsSuccess = false;
                    return authResult;
                }
                UserResult employeeResult = await CreateEmployee(employee);
                if (!employeeResult.IsSuccess)
                {
                    authResult.ErrorMessage = employeeResult.ErrorMessage;
                    return authResult;
                }
                Employee employeeCreated = employeeResult.ObjectResult;
                UserCredential userCredential = employee.UserCredential;
                userCredential.EmployeeId = employeeCreated.EmployeeId;

                authResult = await CreateCredentials(userCredential);
            }
            catch (DbUpdateException ex)
            {
                authResult.ErrorMessage = "An error ocurred while trying to create the new Employee, try again.";
            }
            return authResult;
        }

        private async Task<bool> UserCrentialsExistsByEmail(string email)
        {
            var credentials = await _enterpriseContext
                .UserCredentials
                .FirstOrDefaultAsync(u => u.Email == email);

            return credentials != null;
        }
        private string GetFormattedEmail(UserCredential userCredential)
            => userCredential.Email.FormatEmail();
    }
}

