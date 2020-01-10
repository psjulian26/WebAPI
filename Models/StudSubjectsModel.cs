using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class StudSubjectsModel
    {
        //private string studentno = "";
        public int StudSubId { get; set; }
        public int StudId { get; set; }
        public int SubId { get; set; }
        public string Remarks { get; set; }

        //public string SubName { get; set; }
        //public string SubSubDesc { get; set; }
        [IgnoreDataMember]
        public string StudentNo { get; set; }
        public SubjectsModel SubjectInfo { get; set; }
    }
}
