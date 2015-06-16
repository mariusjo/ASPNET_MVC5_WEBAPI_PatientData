using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
namespace PatientData.Models
{
    public static class PatientDb
    {
        public static IMongoCollection<Patient> Open()
        {
            IMongoCollection<Patient> patients;
            
            try
            {
                 var client = new MongoClient("mongodb://localhost");
                 var db = client.GetDatabase("PatientDb");
                 patients = db.GetCollection<Patient>("Patiens");
                
                //try to access database before returning
                var test = patients.CountAsync(p => true).Result;
                 
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception("Error when trying to access the database!",e);
            }

            return patients;
        }
    }
}