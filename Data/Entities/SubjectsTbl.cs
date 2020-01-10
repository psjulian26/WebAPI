using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data
{
    public  class SubjectsTbl
    {
        public SubjectsTbl()
        {
            //Professors = new HashSet<ProfessorsTbl>();
            StudentSubjectsTbl = new HashSet<StudentSubjectsTbl>();
        }
         
        [Key]
        [Column("SUB_ID")]
        public int SubId { get; set; }
        [Required]
        [Column("SUB_NAME")]
        [StringLength(50)]
        public string SubName { get; set; }
        [Column("SUB_DESC")]
        [StringLength(100)]
        public string SubDesc { get; set; }

        public ProfessorsTbl Professors { get; set; }

        //[InverseProperty("Sub")]
        //public  ICollection<ProfessorsTbl> Professors { get; set; }
        //[InverseProperty("Sub")]
        public  ICollection<StudentSubjectsTbl> StudentSubjectsTbl { get; set; }
               
    }
}
