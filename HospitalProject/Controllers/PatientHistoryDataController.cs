using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using HospitalProject.Models;

namespace HospitalProject.Controllers
{
    public class PatientHistoryDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/PatientHistoryData/ListPatientHistory
        [HttpGet]
        public IEnumerable<PatientHistoryDto> ListPatientHistory()
        {
            List<PatientHistory> PatientHistories = db.PatientHistories.ToList();
            List<PatientHistoryDto> PatientHistoryDtos = new List<PatientHistoryDto>();

            PatientHistories.ForEach(p => PatientHistoryDtos.Add(new PatientHistoryDto()
            {
                PatientHistoryID = p.PatientHistoryID,
                PatientDetails = p.PatientDetails,
                PatientID = p.PatientID,
                PatientName=p.Patient.PatientName,
                Age = p.Patient.Age,
                PhoneNumber = p.Patient.PhoneNumber
            }));

            return PatientHistoryDtos;
        }

        // GET: api/PatientData/FindOnePatientHistory/5
        [ResponseType(typeof(Patient))]
        [HttpGet]
        public IHttpActionResult FindOnePatientHistory(int id)
        {
            PatientHistory patientHistory = db.PatientHistories.Find(id);
            PatientHistoryDto PatientHistoryDto = new PatientHistoryDto()
            {
                PatientHistoryID = patientHistory.PatientHistoryID,
                PatientDetails= patientHistory.PatientDetails,
                PatientID = patientHistory.PatientID,
                PatientName = patientHistory.Patient.PatientName,
                Sex = patientHistory.Patient.Sex,
                Age = patientHistory.Patient.Age,
                PhoneNumber = patientHistory.Patient.PhoneNumber
            };

            if (patientHistory == null)
            {
                return NotFound();
            }

            return Ok(PatientHistoryDto);
        }

        // GET: api/PatientHistoryData/FindPatientHistory/5
        [ResponseType(typeof(PatientHistory))]
        [HttpGet]
        public IEnumerable<PatientHistoryDto> FindPatientHistory(int id)
        {
            List<PatientHistory> patientHistory = db.PatientHistories.Where(p => p.Patient.PatientID == id).ToList();
            List<PatientHistoryDto> PatientHistoryDto = new List<PatientHistoryDto>();

            patientHistory.ForEach(p => PatientHistoryDto.Add(new PatientHistoryDto()
            {
                PatientHistoryID = p.PatientHistoryID,
                PatientDetails = p.PatientDetails,
                PatientID = p.PatientID,
                PatientName = p.Patient.PatientName,
                Age = p.Patient.Age,
                PhoneNumber = p.Patient.PhoneNumber
            }));


            return PatientHistoryDto;
        }

        // POST: api/PatientHistoryData/UpdatePatientHistory/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdatePatientHistory(int id, PatientHistory patientHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patientHistory.PatientHistoryID)
            {
                return BadRequest();
            }

            db.Entry(patientHistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientHistoryExists(id))
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

        // POST: api/PatientHistoryData/AddPatientHistory
        [ResponseType(typeof(PatientHistory))]
        [HttpPost]
        public IHttpActionResult AddPatientHistory(PatientHistory patientHistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PatientHistories.Add(patientHistory);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = patientHistory.PatientHistoryID }, patientHistory);
        }

        // POST: api/PatientHistoryData/DeletePatientHistory/5
        [ResponseType(typeof(PatientHistory))]
        [HttpPost]
        public IHttpActionResult DeletePatientHistory(int id)
        {
            PatientHistory patientHistory = db.PatientHistories.Find(id);
            if (patientHistory == null)
            {
                return NotFound();
            }

            db.PatientHistories.Remove(patientHistory);
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

        private bool PatientHistoryExists(int id)
        {
            return db.PatientHistories.Count(e => e.PatientHistoryID == id) > 0;
        }
    }
}