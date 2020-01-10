using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.Models;

namespace WebAPI.Data
{
    public class StudentsProfile : Profile
    {
        public StudentsProfile()
        {
 
            this.CreateMap<StudentInfoTbl, StudentsModel>().ForMember(c => c.Address, p => p.MapFrom(d => d.Addr.Description)).ReverseMap(); ;

            //this.CreateMap<StudentSubjectsTbl, StudSubjectsModel>().ForMember(e => e.SubName, o => o.MapFrom(f => f.Sub.SubDesc));
            this.CreateMap<SubjectsTbl, SubjectsModel>().ForMember(e => e.ProfName, o => o.MapFrom(f => f.Professors.ProfName)).ReverseMap(); ;

        }
        
      
    }
}
