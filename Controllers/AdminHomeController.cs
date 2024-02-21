using ASPNET_HHRR_Vacations.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNET_HHRR_Vacations.Helpers;
using System.Security.Claims;
using ASPNET_HHRR_Vacations.Services.Authentication;
using ASPNET_HHRR_Vacations.Services.VacationRequests;
using ASPNET_HHRR_Vacations.Services.Employees;

namespace ASPNET_HHRR_Vacations.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminHomeController : Controller
    {
        private readonly EnterpriseContext _enterpriseContext;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IVacationRepository _vacationRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public AdminHomeController(EnterpriseContext enterpriseContext, IAuthService authService, IUserService userService, IVacationRepository vacationRepository, IEmployeeRepository employeeRepository)
        {
            _enterpriseContext = enterpriseContext;
            _authService = authService;
            _userService = userService;
            _vacationRepository = vacationRepository;
            _employeeRepository = employeeRepository;
        }
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                ViewBag.Name = User?.Identity?.Name;
                return View();
            }

            return RedirectToAction("Index", "EmployeeHome");
        }
        public async Task<ActionResult<List<Employee>>> Employees()
        {
            var employees = await _enterpriseContext.Employees
                .Include(e => e.VacationTickets)
                .Include(e => e.UserCredential)
                .ToListAsync();
            return View(employees);
        }
        public async Task<ActionResult<List<VacationTicket>>> Vacations(int id, string completeName)
        {
            TempData["EmployeeName"] = completeName;
            try
            {
                var vacations = await _enterpriseContext.VacationTickets
                .Where(e => e.EmployeeId == id)
                .Include(e => e.Vacation)
                .Include(e => e.Request)
                .ToListAsync();
                return View(vacations);
            }
            catch (Exception ex)
            {
                GenericErrorSetter();
            }
            return NotFound();
        }
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var employeeUpdated = await _employeeRepository.FindByIdAndIncludeCredentials(id);
                if (employeeUpdated == null)
                    return RedirectToAction("Employees");
                return View(employeeUpdated);
            }
            catch (Exception ex)
            {
                GenericErrorSetter();
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<ActionResult> Approve(int id)
        {
            try
            {
                var ticket = await _vacationRepository.FindById(id);
                if (ticket != null)
                {
                    await _vacationRepository.Approve(ticket);
                    TempData["Success"] = "Ticket has been approved successfully.";
                    var parms = new
                    {
                        id = ticket.EmployeeId,
                    };
                    return RedirectToAction("Vacations", parms);
                }
                else
                {
                    DbTicketUpdateErrorSetter();
                }
            }
            catch (Exception ex)
            {
                GenericErrorSetter();
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<ActionResult> Decline(int id)
        {
            try
            {
                VacationTicket ticket = await _vacationRepository.FindById(id);
                if (ticket == null)
                {
                    DbTicketUpdateErrorSetter();
                    return NotFound();
                }
                await _vacationRepository.Decline(ticket);
                TempData["Success"] = "Ticket has been declined successfully.";
                var parms = new
                {
                    id = ticket.EmployeeId,
                };
                return RedirectToAction("Vacations", parms);
            }
            catch (DbUpdateException ex)
            {
                DbTicketUpdateErrorSetter();
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Employee>> Edit(Employee employeeUpdated)
        {
            try
            {
                var employeeDb = await _employeeRepository.FindByIdAndIncludeCredentials(employeeUpdated.EmployeeId);

                if (employeeDb != null)
                {
                    bool isUpdated = false;
                    if (!employeeDb.FirstName.Equals(employeeUpdated.FirstName))
                    {

                        employeeDb.FirstName = employeeUpdated.FirstName;
                        isUpdated = true;
                        TempData["Success"] = "Employee first name has been updated successfully";
                    }

                    if (!employeeDb.LastName.Equals(employeeUpdated.LastName))
                    {
                        employeeDb.LastName = employeeUpdated.LastName;
                        isUpdated = true;
                        TempData["Success"] = "Employee last name has been updated successfully";
                    }
                    if (isUpdated)
                        await _enterpriseContext.SaveChangesAsync();

                }
            }
            catch (DbUpdateException ex)
            {
                DbUpdateErrorSetter();
            }
            catch (Exception ex)
            {
                GenericErrorSetter();
            }
            return View(employeeUpdated);
        }
        public ActionResult ActionConfirmation()
        {
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            UserCredential? employee = await _enterpriseContext.UserCredentials
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee == null)
                return NotFound();

            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<UserCredential>> Delete(UserCredential user)
        {
            try
            {
                int adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
                var adminCredentials = await _enterpriseContext.UserCredentials
                    .FirstOrDefaultAsync(u => u.EmployeeId == adminId && u.IsAdmin);

                if (adminCredentials == null)
                {
                    ModelState.AddModelError(string.Empty, "Admin user not found, try again.");
                    return View("Delete", user);
                }

                if (user.PasswordHash != adminCredentials.PasswordHash)
                {
                    ModelState.AddModelError(string.Empty, "Password doesn't match, try again.");
                    return View("Delete", user);
                }
                var employee = await _employeeRepository.FindByIdAndIncludeCredentials(user.EmployeeId);
                if (employee == null)
                {
                    ModelState.AddModelError(string.Empty, "Employee not found, try again.");
                    return View("Delete", user);
                }


                await _employeeRepository.Delete(employee);
                return RedirectToAction("Employees");
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, "An unkown error occurred while trying to update the employee.");
            }
            catch (NullReferenceException ex)
            {
                ModelState.AddModelError(string.Empty, "Invalid Employee or Admin reference, try again.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                if (ex.InnerException != null)
                {
                    Console.WriteLine("Inner Exception:");
                    Console.WriteLine(ex.InnerException.ToString());
                }
                GenericErrorSetter();
            }
            return View("Delete", user);
        }
        public ActionResult<Employee> Create()
        {
            ViewBag.Name = User?.Identity?.Name;
            Employee viewModel = new();
            viewModel.UserCredential = new();
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee model)
        {
            try
            {
                var result = await _userService.CreateUser(model);

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                }
                else
                {
                    Employee? employee = result.ObjectResult.Employee;
                    TempData["Success"] = $"{employee.CompleteName} has been added successfully.";
                }
            }
            catch (DbUpdateException ex)
            {
                DbUpdateErrorSetter();
            }
            catch (Exception ex)
            {
                GenericErrorSetter();
            }
            return View();
        }
        private void DbTicketUpdateErrorSetter() => ModelState.AddModelError(string.Empty, "An unkown error occurred while trying to update the ticket");
        private void GenericErrorSetter() => ModelState.AddModelError(string.Empty, "An unkown error ocurred. Please try again.");
        private void DbUpdateErrorSetter() => ModelState.AddModelError(string.Empty, "An error ocurred while trying to update the database.");
    }
}
