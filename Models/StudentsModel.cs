using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class StudentsModel
    {
        //private string addr = "-";

        public int StudId { get; set; }
        [Required]
        public string Studno { get; set; }
        [Required]
        public string FName { get; set; }
        [Required]
        public string LName { get; set; }
        [Required]
        public string MName { get; set; }
        [Required]
        public int AddId { get; set; }
        [Required]
        public int StudSubId { get; set; }

        public string Address { get; set; }
        public string AddrState { get; set; }
        public string AddrPostalCode { get; set; }
        //public string Addr { get => addr; set => addr = ""; }

        public ICollection<StudSubjectsModel> StudSub { get; set; }

    }
}
