using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Data   
{
    public  class AddressTbl
    {
        //public AddressTbl()
        //{
        //    StudentInfoTbl = new HashSet<StudentInfoTbl>();
        //}

        [Key]
        [Column("ADD_ID")]
        public int AddId { get; set; }
        [Column("DESCRIPTION")]
        [StringLength(100)]
        public string Description { get; set; }
        [Column("STATE")]
        [StringLength(50)]
        public string State { get; set; }
        [Column("POSTAL_CODE")]
        [StringLength(10)]
        public string PostalCode { get; set; }

        //[InverseProperty("Add")]
        //public  ICollection<StudentInfoTbl> StudentInfoTbl { get; set; }
    }
}
