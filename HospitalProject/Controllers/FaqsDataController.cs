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
    public class FaqsDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/FaqsData/ListFaqs
        [HttpGet]
        public IEnumerable<FaqDto> ListFaqs()
        {
            List<Faq> Faqs = db.Faqs.ToList();
            List<FaqDto> FaqDtos = new List<FaqDto>();
            Faqs.ForEach(m => FaqDtos.Add(new FaqDto()
            {
               FaqId = m.FaqId,
               FaqAnswer = m.FaqAnswer,
               FaqQuestion = m.FaqQuestion,
             
               MedicineName = m.Medicine.MedicineName,
               MedicinePrice = m.Medicine.MedicinePrice,
            }));
            return FaqDtos;
        }

        // GET: api/FaqsData/FindFaq/5
        [ResponseType(typeof(Faq))]
        [HttpGet]
        public IHttpActionResult FindFaq(int id)
        {
            Faq Faq = db.Faqs.Find(id);
            FaqDto FaqDto = new FaqDto()
            {
               FaqId = Faq.FaqId,
               FaqAnswer = Faq.FaqAnswer,
               FaqQuestion = Faq.FaqQuestion,
               MedicinePrice= Faq.Medicine.MedicinePrice,
               MedicineName = Faq.Medicine.MedicineName
            };
            if (Faq == null)
            {
                return NotFound();
            }

            return Ok(FaqDto);
        }

        // PUT: api/FaqsData/UpdateFaq/5
        [ResponseType(typeof(void))]
        [HttpPost]
        public IHttpActionResult UpdateFaq(int id, Faq faq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != faq.FaqId)
            {
                return BadRequest();
            }

            db.Entry(faq).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FaqExists(id))
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

        // POST: api/FaqsData/AddFaq
        [ResponseType(typeof(Faq))]
        [HttpPost]
        public IHttpActionResult AddFaq(Faq faq)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Faqs.Add(faq);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = faq.FaqId }, faq);
        }

        // DELETE: api/FaqsData/DeleteFaq/5
        [ResponseType(typeof(Faq))]
        [HttpPost]
        public IHttpActionResult DeleteFaq(int id)
        {
            Faq faq = db.Faqs.Find(id);
            if (faq == null)
            {
                return NotFound();
            }

            db.Faqs.Remove(faq);
            db.SaveChanges();

            return Ok(faq);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FaqExists(int id)
        {
            return db.Faqs.Count(e => e.FaqId == id) > 0;
        }
    }
}