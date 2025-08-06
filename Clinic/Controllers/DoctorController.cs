using Clinic.Data;
using Clinic.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;



namespace Clinic.Controllers
{
    public class DoctorController : Controller
    {

        ApplicationDbContext db = new ApplicationDbContext();

        //action for viewing the doctors and there information
        // should contain button to show the details for each doctor in separate page
        public IActionResult Index()
        {
            var doctors = db.Doctors.Where(a=> a.Specialization != "Admin").ToList();
            return View(doctors);
        }

        //Action to details of one person
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var doctor = db.Doctors.SingleOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);

        }

        // Create Action to show the View of Create Form
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Create Action to Create the Doctor
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Doctor doc)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            db.Add(doc);
            db.SaveChanges();
            return RedirectToAction("index");
        }

        // Create Action to show the View of Create Form
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var doctor = db.Doctors.SingleOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        //Edit Action to Edit the Doctor details
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Doctor doc)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.Update(doc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var doctor = db.Doctors.SingleOrDefault(d => d.Id == id);
            return View(doctor);
        }

        // action to delete the doctor
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ActionName("Delete")]

        public IActionResult DeleteConfirm(int id)
        {
            var doctor = db.Doctors.SingleOrDefault(d => d.Id == id);

            if (doctor == null)
            {
                return NotFound();
            }

            db.Doctors.Remove(doctor);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
