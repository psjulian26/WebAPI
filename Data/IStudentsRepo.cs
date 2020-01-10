using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace WebAPI.Data
{
    public interface IStudentsRepo
    {

        // General 
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        //Record filter
        Task<StudentInfoTbl[]> GetAll(bool includeSubjects = false);
        Task<StudentInfoTbl> GetStudentsRecByStudno(string studno, bool includeSubjects = false);
        Task<StudentInfoTbl> GetStudent(string studno, bool includeSubjects = false);
        Task<StudentInfoTbl> GetStudentsRec(string studno, bool includeSubjects = false);

        Task<StudentSubjectsTbl[]> GetSubsByStudno(string studno, bool includeSubjects = false);
        Task<StudentSubjectsTbl> GetSubByStudno(string studno, int subid, bool includeSubjects = false);


        Task<SubjectsTbl> GetSubject(int subjectID);

        //Task<StudentSubjectsTbl[]> GetSubByLname(string Lname, bool includeProf = false);

    }
}
