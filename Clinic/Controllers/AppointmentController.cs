using Clinic.Data;
using Clinic.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;

namespace Clinic.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: /Appointment
        public IActionResult Index()
        {
            using var db = new ApplicationDbContext();

            var doctorId = Request.Cookies["ID"];
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            List<Appointment> appointments;
            if (userRole == "Admin")
            {
                // Admin: see all appointments
                appointments = db.Appointments
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .OrderBy(a => a.AppointmentDate)
                    .ToList();
            }
            else
            {
                // Doctor: see only their own appointments
                appointments = db.Appointments
                    .Where(x => x.DoctorId.ToString() == doctorId)
                    .Include(a => a.Patient)
                    .Include(a => a.Doctor)
                    .OrderBy(a => a.AppointmentDate)
                    .ToList();
            }
            return View(appointments);
        }

        // GET: /Appointment/Details/5
        public IActionResult Details(int id)
        {
            using var db = new ApplicationDbContext();
            var appt = db.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefault(a => a.Id == id);

            if (appt == null)
                return NotFound();

            return View(appt);
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "MyCookieAuth")] //Admin Can Create Appointment 
        public IActionResult Create()
        {
            var model = new Appointment { AppointmentDate = DateTime.Now.AddHours(1) };

            using (var db = new ApplicationDbContext())
            {

                var patients = db.Patients
                                 .OrderBy(p => p.FullName)
                                 .Select(p => new { p.Id, p.FullName })
                                 .ToList();
                var doctors = db.Doctors
                                .Where(d => d.Specialization != "Admin")
                                .OrderBy(d => d.FullName)
                                .Select(d => new { d.Id, d.FullName })
                                .ToList();

                ViewBag.Patients = new SelectList(patients, "Id", "FullName", model.PatientId);
                ViewBag.Doctors = new SelectList(doctors, "Id", "FullName", model.DoctorId);
            }

            return View(model);
        }

        // POST: /Appointment/Create
        [HttpPost]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "MyCookieAuth")] //Admin Can Create Appointment 
        public IActionResult Create(Appointment model)
        {
            if (!ModelState.IsValid)
            {
                using var db = new ApplicationDbContext();
                ViewBag.Patients = new SelectList(
                    db.Patients.OrderBy(p => p.FullName), "Id", "FullName", model.PatientId);
                ViewBag.Doctors = new SelectList(
                    db.Doctors.OrderBy(d => d.FullName), "Id", "FullName", model.DoctorId);
                return View(model);
            }

            using var db2 = new ApplicationDbContext();
            db2.Appointments.Add(model);
            db2.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin", AuthenticationSchemes = "MyCookieAuth")] //Admin Can Edit Appointment 
        public IActionResult Edit(int id)
        {
            Appointment model;
            using (var db = new ApplicationDbContext())
            {
                model = db.Appointments.Find(id);
                if (model == null) return NotFound();

                var patients = db.Patients
                                 .OrderBy(p => p.FullName)
                                 .Select(p => new { p.Id, p.FullName })
                                 .ToList();
                var doctors = db.Doctors
                                .OrderBy(d => d.FullName)
                                .Select(d => new { d.Id, d.FullName })
                                .ToList();

                ViewBag.Patients = new SelectList(patients, "Id", "FullName", model.PatientId);
                ViewBag.Doctors = new SelectList(doctors, "Id", "FullName", model.DoctorId);
            }

            return View(model);
        }


        // POST: /Appointment/Edit
        [HttpPost]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "MyCookieAuth")] //Admin Can Edit Appointment 
        public IActionResult Edit(Appointment model)
        {
            if (!ModelState.IsValid)
            {
                using (var db = new ApplicationDbContext())
                {
                    var patients = db.Patients.OrderBy(p => p.FullName)
                                             .Select(p => new { p.Id, p.FullName }).ToList();
                    var doctors = db.Doctors.OrderBy(d => d.FullName)
                                             .Select(d => new { d.Id, d.FullName }).ToList();

                    ViewBag.Patients = new SelectList(patients, "Id", "FullName", model.PatientId);
                    ViewBag.Doctors = new SelectList(doctors, "Id", "FullName", model.DoctorId);
                }
                return View(model);
            }

            using (var db = new ApplicationDbContext())
            {
                db.Appointments.Update(model);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: /Appointment/Delete/5
        [Authorize(Roles = "Admin", AuthenticationSchemes = "MyCookieAuth")] //Admin Can Delete Appointment 
        public IActionResult Delete(int id)
        {
            using var db = new ApplicationDbContext();
            var appt = db.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .FirstOrDefault(a => a.Id == id);

            if (appt == null)
                return NotFound();

            return View(appt);
        }

        // POST: /Appointment/Delete/5
        [Authorize(Roles = "Admin", AuthenticationSchemes = "MyCookieAuth")] //Admin Can Delete Appointment 
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            using var db = new ApplicationDbContext();
            var appt = db.Appointments.Find(id);
            if (appt != null)
            {
                db.Appointments.Remove(appt);
                db.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
