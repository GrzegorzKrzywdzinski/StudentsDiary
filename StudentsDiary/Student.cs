using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDiary
{
    public class Student
    {

        private delegate void DisplayMessage(string message);

        public int Id { get; set; } 
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Math { set; get; }
        public string Technology { get; set; }    
        public string Physics { set; get; }  
        public string PolishLang { set; get; }  
        public string ForeignLang { set; get; }
        public string Comments { set; get; }
        public bool IsExtraLessons { set; get; }    
        public string IdGroup { set; get; } 

    }
}
