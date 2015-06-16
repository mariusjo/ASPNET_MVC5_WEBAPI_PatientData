using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PatientData.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Web.Http.Cors;

namespace PatientData.Controllers
{
    [EnableCors("*","*","GET")]
    [Authorize]
    public class PatientsController : ApiController
    {
        IMongoCollection<Patient> _patients;

        public PatientsController()
        {
            try
            {
                _patients = PatientDb.Open();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                throw new Exception("Error when trying to retrieve patient data for query!", e);
            }
        }

        public IEnumerable<Patient> Get()
        {
            return _patients.Find<Patient>(p => true).ToListAsync().Result;
        }
        public IHttpActionResult Get(string id)
        {
            Patient patient;
            try
            {
                ObjectId.Parse(id);
            }
            catch
            {
                return NotFound();
            }

            patient = _patients.Find(p => p.Id == id).FirstAsync().Result;

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        [Route("api/patients/{id}/medications")]
        public IHttpActionResult GetMedications(string id)
        {
            Patient patient;
            try
            {
                ObjectId.Parse(id);
            }
            catch
            {
                return NotFound();
            }

            patient = _patients.Find(p => p.Id == id).FirstOrDefaultAsync().Result;

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient.Medications);
        }

    }
}
