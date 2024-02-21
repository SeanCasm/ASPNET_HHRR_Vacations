using ASPNET_HHRR_Vacations.Models;
using ASPNET_HHRR_Vacations.Services.VacationRequests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASPNET_HHRR_Vacations.Controllers
{
    [Authorize(Roles = "User")]
    public class EmployeeHomeController : Controller
    {
        private readonly EnterpriseContext _enterpriseContext;
        private readonly IVacationRepository _vacationRepository;
        public EmployeeHomeController(EnterpriseContext enterpriseContext, IVacationRepository vacationRepository)
        {
            _enterpriseContext = enterpriseContext;
            _vacationRepository = vacationRepository;
        }

        public ActionResult Index()
        {
            if (User.IsInRole("User"))
            {
                ViewBag.Name = User?.Identity?.Name;
                return View();
            }

            return RedirectToAction("Index", "AdminHome");
        }
        public ActionResult CreateTicket()
        {
            int id = GetId();
            if (id != null)
            {
                Vacation vacation = new()
                {
                    EmployeeId = id
                };
                return View(vacation);
            }
            return View();
        }
        public async Task<ActionResult> Vacations()
        {
            int id = GetId();
            var vacations = await _vacationRepository.GetVacationsTickets(id);
            return View(vacations);
        }
        [HttpPost]
        public async Task<ActionResult> CreateTicket(Vacation vacation)
        {
            if (vacation.StartDate >= vacation.EndDate)
            {
                ModelState.AddModelError(string.Empty, "End date must be greater than start date");
                return View(vacation);
            }

            try
            {
                await _vacationRepository.CreateVacationTicket(vacation);
                TempData["Success"] = "Vacations request has been successfully issued.";
            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError(string.Empty, "An error ocurred while trying to send your request. Please try again.");
            }
            return View(vacation);
        }
        [NonAction]
        private int GetId()
        {
            var idString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(idString);
        }
    }
}
