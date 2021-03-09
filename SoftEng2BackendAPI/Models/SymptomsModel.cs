using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftEng2BackendAPI.Models
{
    public class SymptomsModel
    {
        public int Symptoms_ID { get; set; }
        public int User_ID { get; set; }
        public string Symptoms_Name { get; set; }
        //For fetching Data from db
        public SymptomsModel(int symptoms_ID, int user_ID, string symptoms_Name)
        {
            Symptoms_ID = symptoms_ID;
            User_ID = user_ID;
            Symptoms_Name = symptoms_Name;
        }
        //Inserting new data to db
        public SymptomsModel(int user_ID, string symptoms_Name)
        {
            User_ID = user_ID;
            Symptoms_Name = symptoms_Name;
        }

        public SymptomsModel()
        {
        }
    }
}
