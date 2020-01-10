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
    [Route("api/students/{studno}/subjects")]
    [ApiController]
    
    //[Route("api/v{version:apiVersion}/students/{studno}/subjects")]
    public class SubjectController : ControllerBase
    { 
        private readonly IStudentsRepo _repo;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public SubjectController(IStudentsRepo repo, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repo = repo;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        // GET
        [HttpGet]
        public async Task<ActionResult<StudSubjectsModel[]>> Get(string studno)
        {
            //try
            //{
                var subjects = await _repo.GetSubsByStudno(studno, true);
                return _mapper.Map<StudSubjectsModel[]>(subjects);
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Subjects");
            //}
        }

        //Get by stud
        [HttpGet("{id:int}")]
        public async Task<ActionResult<StudSubjectsModel>> Get(string studno, int id)
        {
            try
            {
                var subject = await _repo.GetSubByStudno(studno, id, true);
                if (subject == null) return NotFound($"Couldn't find a subject with id of {id}");
                return _mapper.Map<StudSubjectsModel>(subject);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get subject.");
            }
        }


        // POST 
        [HttpPost]
        public async Task<ActionResult<StudSubjectsModel>> Post(string studno, StudSubjectsModel model)
        {
            //try
            //{
                var student = await _repo.GetStudentsRec(studno);
                if (student == null) return BadRequest("Student does not exist");

                var StudentSubs = _mapper.Map<StudentSubjectsTbl>(model);
                StudentSubs.StudentNo = student;

                //if (model.StudId == 0) return BadRequest("Student ID is required");
                if (model.SubId == 0) return BadRequest("Subject ID is required");

                var subjects = await _repo.GetSubject(model.SubId);
                if (subjects == null) return BadRequest("Subject could not be found");
                StudentSubs.SubjectInfo = subjects;

                _repo.Add(StudentSubs);

                if (await _repo.SaveChangesAsync())
                {
                    var url = _linkGenerator.GetPathByAction(HttpContext,
                      "Get",
                      values: new { studno, id = StudentSubs.StudentNo });

                    return Created(url, _mapper.Map<StudSubjectsModel>(StudentSubs));
                }
                else
                {
                    return BadRequest("Failed to save new Subject");
                }
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Talks");
            //}
        }

        // PUT 
        [HttpPut("{subjectID:int}")]
        public async Task<ActionResult<StudSubjectsModel>> Put(string studno, int subjectID, StudSubjectsModel model)
        {
            //try
            //{
            var subject = await _repo.GetSubByStudno(studno, subjectID, true);
            if (subject == null) return NotFound("Couldn't find the subject");

            _mapper.Map(model, subject);

            if (model.SubId != 0)
            {
                var subsInfo = await _repo.GetSubject(model.SubId);
                if (subsInfo != null)
                {
                    subject.SubjectInfo = subsInfo;
                }
            }

            if (await _repo.SaveChangesAsync())
            {
                return _mapper.Map<StudSubjectsModel>(subject);
            }
            else
            {
                return BadRequest("Failed to update database.");
            }
            //}
            //catch (Exception)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Talks");
            //}
        }

        // DELETE 
        [HttpDelete("{subid:int}")]
        public async Task<IActionResult> Delete(string studno, int subid)
        {
            try
            {
                var subject = await _repo.GetSubByStudno(studno, subid);
                if (subject == null) return NotFound("Failed to find the subject to delete");
                _repo.Delete(subject);

                if (await _repo.SaveChangesAsync())
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to delete talk");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get Talks");
            }
        }

    }
}

