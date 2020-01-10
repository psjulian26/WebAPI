using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data
{
    public  class StudentInfoTbl
    {
        [Key]
        [Column("STUD_ID")]
        public int StudId { get; set; }
        [Column("STUD_NO")]
        [StringLength(10)]
        public string Studno { get; set; }
        [Column("F_NAME")]
        [StringLength(50)]
        public string FName { get; set; }
        [Column("L_NAME")]
        [StringLength(50)]
        public string LName { get; set; }
        [Column("M_NAME")]
        [StringLength(50)]
        public string MName { get; set; }
        [Column("ADD_ID")]
        public int AddId { get; set; }
        [Column("STUD_SUB_ID")]
        public int StudSubId { get; set; }

        [ForeignKey("AddId")]
        //[InverseProperty("StudentInfoTbl")]
        public  AddressTbl Addr { get; set; }
        public ICollection<StudentSubjectsTbl> StudSub { get; set; }
    }
}
