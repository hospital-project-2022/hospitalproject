﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalProject.Models;
using System.Diagnostics;

namespace HospitalProject.Controllers
{
    public class PatientDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// Fetches all the patients from the database
        /// </summary>

        // GET: api/PatientData/ListPatients
        [HttpGet]
        public IEnumerable<PatientDto> ListPatients()
        {
            List<Patient> Patients = db.Patients.ToList();
            List<PatientDto> PatientDtos= new List<PatientDto>();

            Patients.ForEach(p => PatientDtos.Add(new PatientDto()
            {
                PatientID = p.PatientID,
                PatientName = p.PatientName,
                Sex = p.Sex,
                Age = p.Age,
                DateOfBirth = p.DateOfBirth,
                PhoneNumber = p.PhoneNumber
            }));

            return PatientDtos;

        }

        /// <summary>
        /// To find a prticular patient detail from the database
        /// </summary>
        /// <param name="id">Patient ID</param>
        /// <returns>Returns detail of the selected patient ID</returns>

        // GET: api/PatientData/FindPatient/5
        [ResponseType(typeof(Patient))]
        [HttpGet]
        public IHttpActionResult FindPatient(int id)
        {
            Patient patient = db.Patients.Find(id);
            PatientDto PatientDto = new PatientDto()
            {
                PatientID = patient.PatientID,
                PatientName = patient.PatientName,
                Sex = patient.Sex,
                Age = patient.Age,
                DateOfBirth = patient.DateOfBirth,
                PhoneNumber = patient.PhoneNumber
            };

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(PatientDto);
        }

        /// <summary>
        /// Updates details of the selected patient into the database
        /// </summary>
        /// <param name="id">Selected Patient ID</param>
        /// <param name="patient">Patient Details</param>
        /// <returns>Updates the database with the modified data</returns>
        
        // POST: api/PatientData/UpdatePatient/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePatient(int id, Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.PatientID)
            {
                return BadRequest();
            }

            db.Entry(patient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds new patient into the database
        /// </summary>
        /// <param name="patient">Patient information</param>

        // POST: api/PatientData/AddPatient
        [ResponseType(typeof(Patient))]
        [HttpPost]
        public IHttpActionResult AddPatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Patients.Add(patient);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = patient.PatientID }, patient);
        }

        /// <summary>
        /// Deletes patient from the database
        /// </summary>
        /// <param name="id">Selected Patient ID for deletion</param>

        // POST: api/PatientData/DeletePatient/5
        [ResponseType(typeof(Patient))]
        [HttpPost]
        public IHttpActionResult DeletePatient(int id)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            db.Patients.Remove(patient);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(int id)
        {
            return db.Patients.Count(e => e.PatientID == id) > 0;
        }
    }
}