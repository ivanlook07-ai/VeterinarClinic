using Microsoft.AspNetCore.Mvc;
using VeterinaryClinic.Data;
using VeterinaryClinic.Models;

namespace VeterinaryClinic.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var patients = _context.Patients.ToList();
            return View(patients);
        }

        [HttpPost]
        public IActionResult Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.Add(patient);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Index", _context.Patients.ToList());
        }
    }
}