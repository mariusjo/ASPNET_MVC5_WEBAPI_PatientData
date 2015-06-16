using PatientData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace PatientData.App_Start
{
    public static class MongoConfig
    {
        public static void Seed()
        {
            IMongoCollection<Patient> patients;
            try
            {
                patients = PatientDb.Open();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                throw new Exception("Error when trying to retrieve patient data for seeding!", e);
            }

            if (patients.Find(p => p.Name == "Marius").ToListAsync().Result.Count <= 0)
            {
                var data = new List<Patient>()
                {
                    new Patient{ 
                        Name = "Marius",
                        Ailments = new List<Ailment>(){ new Ailment{ Name = "Crazy"} },
                        Medications = new List<Medication>(){ new Medication{ Name = "Lalala pills", Doses=42} }
                    },
                    new Patient{ 
                        Name = "Scott",
                        Ailments = new List<Ailment>(){ new Ailment{ Name = "Crazy"} },
                        Medications = new List<Medication>(){ new Medication{ Name = "wiiiiii pills", Doses=27} }
                    },
                    new Patient{ 
                        Name = "Sarah",
                        Ailments = new List<Ailment>(){ new Ailment{ Name = "Crazy"} },
                        Medications = new List<Medication>(){ new Medication{ Name = "tickticktick pills", Doses=1337} }
                    }
                };

                patients.InsertManyAsync(data);

            }

        }
    }
}