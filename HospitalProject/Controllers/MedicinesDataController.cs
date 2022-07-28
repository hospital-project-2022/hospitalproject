
using System;
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

namespace HospitalProject.Controllers
{
    public class MedicinesDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/MedicinesData/ListMedicines
        [HttpGet]
        public IEnumerable<MedicineDto> ListMedicines()
        {
            List<Medicine> Medicines =db.Medicines.ToList();
            List<MedicineDto> MedicineDtos = new List<MedicineDto>();
            Medicines.ForEach(m => MedicineDtos.Add(new MedicineDto()
            {
                MedicineId = m.MedicineId,
                MedicineName = m.MedicineName,
                MedicinePrice = m.MedicinePrice
            }));
            return MedicineDtos;
        }

        // GET: api/MedicinesData/FindMedicine/5
        [ResponseType(typeof(Medicine))]
        [HttpGet]
        public IHttpActionResult FindMedicine(int id)
        {
            Medicine Medicine = db.Medicines.Find(id);
            MedicineDto MedicineDto= new MedicineDto()
            {
                MedicineId = Medicine.MedicineId,
                MedicineName = Medicine.MedicineName,
                MedicinePrice = Medicine.MedicinePrice
            };
            if (Medicine == null)
            {
                return NotFound();
            }

            return Ok(MedicineDto);
        }

        // PUT: api/MedicinesData/UpdateMedicine/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateMedicine(int id, Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medicine.MedicineId)
            {
                return BadRequest();
            }

            db.Entry(medicine).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicineExists(id))
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

        // POST: api/MedicinesData/AddMedicine
        [ResponseType(typeof(Medicine))]
        [HttpPost]
        public IHttpActionResult AddMedicine(Medicine medicine)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Medicines.Add(medicine);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = medicine.MedicineId }, medicine);
        }

        // DELETE: api/MedicinesData/DeleteMedicine/5
        [ResponseType(typeof(Medicine))]
        [HttpPost]
        public IHttpActionResult DeleteMedicine(int id)
        {
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return NotFound();
            }

            db.Medicines.Remove(medicine);
            db.SaveChanges();

            return Ok(medicine);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MedicineExists(int id)
        {
            return db.Medicines.Count(e => e.MedicineId == id) > 0;
        }
    }
}