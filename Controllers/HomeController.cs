using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Data;
using VeterinaryClinic.Models;
using System.Diagnostics;
using System.Linq;

namespace VeterinaryClinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string typeFilter = "", string searchFilter = "")
        {
            var patients = GetFilteredPatients(typeFilter, searchFilter);
            var stats = GetPatientStats();

            var viewModel = new PatientViewModel
            {
                Patients = patients,
                TypeFilter = typeFilter,
                SearchFilter = searchFilter,
                TotalPatients = stats.Total,
                DogsCount = stats.Dogs,
                CatsCount = stats.Cats
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPatient(Patient patient)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //patient.CreatedDate = DateTime.Now;
                    _context.Patients.Add(patient);
                    _context.SaveChanges();
                    TempData["Message"] = "Пациент успешно добавлен!";
                    TempData["IsSuccess"] = true;
                }
                catch (Exception ex)
                {
                    TempData["Message"] = $"Ошибка: {ex.Message}";
                    TempData["IsSuccess"] = false;
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePatient(int id)
        {
            try
            {
                var patient = _context.Patients.Find(id);
                if (patient != null)
                {
                    _context.Patients.Remove(patient);
                    _context.SaveChanges();
                    TempData["Message"] = "Пациент удален!";
                    TempData["IsSuccess"] = true;
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"Ошибка удаления: {ex.Message}";
                TempData["IsSuccess"] = false;
            }
            return RedirectToAction("Index");
        }

        private List<Patient> GetFilteredPatients(string typeFilter, string searchFilter)
        {
            IQueryable<Patient> query = _context.Patients;

            if (!string.IsNullOrEmpty(typeFilter))
                query = query.Where(p => p.Species == typeFilter);

            if (!string.IsNullOrEmpty(searchFilter))
                query = query.Where(p => p.Name.Contains(searchFilter));

            return query.OrderBy(p => p.Name).ToList();
        }

        private (int Total, int Dogs, int Cats) GetPatientStats()
        {
            var total = _context.Patients.Count();
            var dogs = _context.Patients.Count(p => p.Species == "Собака");
            var cats = _context.Patients.Count(p => p.Species == "Кошка");
            return (total, dogs, cats);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class PatientViewModel
    {
        public List<Patient> Patients { get; set; } = new List<Patient>();
        public string TypeFilter { get; set; }
        public string SearchFilter { get; set; }
        public int TotalPatients { get; set; }
        public int DogsCount { get; set; }
        public int CatsCount { get; set; }
    }
}