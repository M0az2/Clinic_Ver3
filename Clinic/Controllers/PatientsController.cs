using Clinic.Data;
using Clinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Clinic.Controllers
{
    [Authorize(Roles = "Admin", AuthenticationSchemes = "MyCookieAuth")] //Admin Can Create Patient 
    public class PatientController : Controller
    {
        public IActionResult Index()
        {
            using (var ss = new ApplicationDbContext())
            {
                var list = ss.Patients.OrderBy(p => p.FullName).ToList();
                return View(list);
            }
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "MyCookieAuth")] //Admin Can Create Patient 
        public IActionResult Create()
        {
            return View(new Patient());
        }

        [HttpPost]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "MyCookieAuth")] // POST: Save patient
        public IActionResult Create(Patient patient)
        {
            if (!ModelState.IsValid)
                return View(patient);

            using (var ss = new ApplicationDbContext())
            {
                ss.Patients.Add(patient);
                ss.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            using (var ss = new ApplicationDbContext())
            {
                var pat = ss.Patients.Find(id);
                if (pat == null) return NotFound();
                return View(pat);
            }
        }

        [HttpPost]
        public IActionResult Edit(Patient patient)
        {
            if (!ModelState.IsValid)
                return View(patient);

            using (var ss = new ApplicationDbContext())
            {
                ss.Patients.Update(patient);
                ss.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            using (var ss = new ApplicationDbContext())
            {
                var pat = ss.Patients.Find(id);
                if (pat == null) return NotFound();
                return View(pat);
            }
        }

        public IActionResult Delete(int id)
        {
            using (var ss = new ApplicationDbContext())
            {
                var pat = ss.Patients.Find(id);
                if (pat == null) return NotFound();
                return View(pat);
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            using (var ss = new ApplicationDbContext())
            {
                var pat = ss.Patients.Find(id);
                ss.Patients.Remove(pat);
                ss.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
