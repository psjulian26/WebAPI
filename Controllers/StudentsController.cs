using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebAPI.Data;
using WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace WebAPI.Controllers
{
    //[Route("api/v{version:apiVersion}/students")]
    //[ApiVersion("1.0")]
    //[ApiVersion("1.1")]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsRepo _repo;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public StudentsController(IStudentsRepo repo,IMapper mapper,LinkGenerator linkGenerator)
        {  
            _repo = repo;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        // GET api/Students
        [HttpGet]
  
        public async Task<ActionResult<StudentsModel[]>> Get(bool includeSubjects = false)
        {
            //try
            //{
            var results = await _repo.GetAll(includeSubjects);
            return _mapper.Map<StudentsModel[]>(results);

            //}
            //catch (Exception)
            //{
            //    return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure -Talkss");
            //    //return Ok("Under Maintenance.");
            //}
        }
        //Get by studno
        [HttpGet("{studno}")]
        public async Task<ActionResult<StudentsModel>> Get(string studno, bool includeSubjects = false)
        {
            //try
            //{
                var result = await _repo.GetStudentsRecByStudno(studno, includeSubjects);

                if (result == null) return NotFound();

                return _mapper.Map<StudentsModel>(result);

            //}
            //catch (Exception)
            //{
            //    return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure -moniker");
            //}
        }
        //[HttpGet("{studno}")]
        //[MapToApiVersion("1.1")]
        //public async Task<ActionResult<StudentsModel>> Get11(string studno, bool includeSubjects = false)
        //{
        //    //try
        //    //{
        //    var result = await _repo.GetStudentsRecByStudno(studno, includeSubjects);

        //    if (result == null) return NotFound();

        //    return _mapper.Map<StudentsModel>(result);

        //    //}
        //    //catch (Exception)
        //    //{
        //    //    return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure -moniker");
        //    //}
        //}

        // POST api/students
        public async Task<ActionResult<StudentsModel[]>> Post(StudentsModel model)
        {

            //try
            //{
            var existing = await _repo.GetStudentsRec(model.Studno);
            if (existing != null)
            {
                return BadRequest("Student No. in Use");
            }

            var dstudno = _linkGenerator.GetPathByAction("Get",
              "Students",
              new { studno = model.Studno });

            if (string.IsNullOrWhiteSpace(dstudno))
            {
                return BadRequest("Could not use current Student No.");
            }

            //// Create a new Camp
            var StudentInfo = _mapper.Map<StudentInfoTbl>(model);
            _repo.Add(StudentInfo);
            if (await _repo.SaveChangesAsync())
            {
                return Created($"/api/sutdents/{StudentInfo.Studno}", _mapper.Map<StudentsModel>(StudentInfo));
            }

            //}
            //catch (Exception)
            //{
            //    return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure - Post");
            //}

            return BadRequest();
        }

        // PUT api/values/5
        [HttpPut("{studno}")]
        public async Task<ActionResult<StudentsModel>> Put(string studno, StudentsModel model)
        {
            //try
            //{
   
                var oldStud = await _repo.GetStudentsRec(studno);
                if (oldStud == null) return NotFound($"Could not find Student with Student No. of {studno}");

                _mapper.Map(model, oldStud);

                if (await _repo.SaveChangesAsync())
                {
                    return _mapper.Map<StudentsModel>(oldStud);
                }
            //}
            //catch (Exception)
            //{
            //    return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure -PUT");
            //}

            return BadRequest();
        }
        // DELETE api/values/5
        [HttpDelete("{studno}")]
        public async Task<IActionResult> Delete(string studno)
        {
            try
            {
                var oldStud = await _repo.GetStudentsRec(studno);
                if (oldStud == null) return NotFound();

                _repo.Delete(oldStud);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }

            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure -DELETE");
            }

            return BadRequest("Failed to delete the camp");
        }
    }
}
