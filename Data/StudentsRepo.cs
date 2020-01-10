using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebAPI.Data
{
    public class StudentsRepo : IStudentsRepo
    {
        private readonly StudentsContext _context;
        private readonly ILogger<StudentsRepo> _logger;

        public StudentsRepo(StudentsContext context,ILogger<StudentsRepo> logger )
        {
            _context = context;
            _logger = logger;
        }

        public StudentsRepo()
        {
        }

        public void Add<T>(T entity) where T : class
        {
            _logger.LogInformation($"Adding an object of type {entity.GetType()} to the context.");
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _logger.LogInformation($"Removing an object of type {entity.GetType()} to the context.");
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Attempitng to save the changes in the context");

            // Only return success if at least one row was changed
            return (await _context.SaveChangesAsync()) > 0;
        }

        //Get All records
        public async  Task<StudentInfoTbl[]> GetAll(bool includeSubjects = false)
        {
            _logger.LogInformation($"Getting all Students");
            IQueryable<StudentInfoTbl> query = _context.StudentInfoTbl
            .Include(a => a.Addr);


            if (includeSubjects)
            {
                query = query
                .Include(d => d.StudSub)
                .ThenInclude(s => s.SubjectInfo)
                .ThenInclude(w => w.Professors);
            } 

            // Order It
            query = query.OrderByDescending(a => a.StudId);

             return await query.ToArrayAsync();

        }
        public async Task<StudentInfoTbl> GetStudent(string studno, bool includeSubjects = false)
        {
            _logger.LogInformation($"Getting a Student for {includeSubjects}");

            IQueryable<StudentInfoTbl> query = _context.StudentInfoTbl
                .Include(c => c.Addr);

            if (includeSubjects)
            {
                query = query
                .Include(c => c.StudSub)
                .ThenInclude(s => s.SubjectInfo)
                .ThenInclude(w => w.Professors);
            }

            // Query It
            query = query.Where(c => c.Studno == studno);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<StudentInfoTbl> GetStudentsRecByStudno(string studno, bool includeSubjects = false)
        {
            _logger.LogInformation($"Getting a record for {studno}");

            IQueryable<StudentInfoTbl> query = _context.StudentInfoTbl
                .Include(c => c.Addr);

            if (includeSubjects)
            {
                query = query
                .Include(c => c.StudSub)
                .ThenInclude(s => s.SubjectInfo)
                .ThenInclude(w => w.Professors);
            }

            // Query It
            query = query.Where(c => c.Studno == studno);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<StudentInfoTbl> GetStudentsRec(string studno, bool includeSubjects = false)//- for post
        {
            _logger.LogInformation($"Getting a record for {studno}");

            IQueryable<StudentInfoTbl> query = _context.StudentInfoTbl
                .Include(c => c.Addr);

            if (includeSubjects)
            {
                query = query
                .Include(d => d.StudSub)
                .ThenInclude(s => s.SubjectInfo)
                .ThenInclude(w => w.Professors);
            }

            // Query It
            query = query.Where(c => c.Studno == studno);

            return await query.FirstOrDefaultAsync();
        }

        //Subs
        public async Task<StudentSubjectsTbl[]> GetSubsByStudno(string studno, bool includeSubjects = false)
        {
            _logger.LogInformation($"Getting all Subjects for a student");

            IQueryable<StudentSubjectsTbl> query = _context.StudentSubjectsTbl;

            if (includeSubjects)
            {
                query = query
                  .Include(t => t.SubjectInfo)
                  .ThenInclude(w => w.Professors);
            }

            // Add Query
            query = query
              .Where(t => t.StudentNo.Studno == studno)
              .OrderByDescending(t => t.SubId);

            return await query.ToArrayAsync();
        }

        public async Task<StudentSubjectsTbl> GetSubByStudno(string studno, int subid, bool includeSubjects = false)
        {
            _logger.LogInformation($"Getting all Subjects for a student");

            IQueryable<StudentSubjectsTbl> query = _context.StudentSubjectsTbl;

            if (includeSubjects)
            {
                query = query
                  .Include(t => t.SubjectInfo)
                  .ThenInclude(w => w.Professors);
            }

            // Add Query
            query = query
              .Where(t => t.SubId == subid && t.StudentNo.Studno == studno);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<SubjectsTbl> GetSubject(int subjectID)
        {
            _logger.LogInformation($"Getting Subject");

            var query = _context.SubjectsTbl
              .Where(t => t.SubId == subjectID);

            return await query.FirstOrDefaultAsync();
        }

      

    }
}
