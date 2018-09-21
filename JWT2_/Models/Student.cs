using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Models
{
    public class Student
    {
        public int IdStudent { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string LastNameM { get; set; }
        public int Status { get; set; }
        public DateTime DOB { get; set; }
    }
}