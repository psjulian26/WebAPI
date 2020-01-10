using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data
{
    public  class StudentSubjectsTbl
    {
        [Key]
        [Column("STUD_SUB_ID")]
        public int StudSubId { get; set; }
        [Column("STUD_ID")]
        public int StudId { get; set; }
        [Column("SUB_ID")]
        public int SubId { get; set; }
        [Column("REMARKS")]
        [StringLength(100)]
        public string Remarks { get; set; }

        public SubjectsTbl SubjectInfo { get; set; }


        //[ForeignKey("SubId")]
        //[InverseProperty("StudentSubjectsTbl")]
        public StudentInfoTbl StudentNo { get; set; }





    }
}
