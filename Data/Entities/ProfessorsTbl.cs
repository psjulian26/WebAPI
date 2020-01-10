using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data
{
    public  class ProfessorsTbl
    {
        [Key]
        [Column("PROF_ID")]
        public int ProfId { get; set; }
        [Column("PROF_NAME")]
        [StringLength(100)]
        public string ProfName { get; set; }
        [Column("SUB_ID")]
        public int SubId { get; set; }

        [ForeignKey("SubId")]
        //[InverseProperty("ProfessorsTbl")]
        public  SubjectsTbl Sub { get; set; }
    }
}
