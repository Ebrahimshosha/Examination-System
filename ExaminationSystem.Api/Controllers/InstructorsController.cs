using ExaminationSystem.Api.Extentions;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.ViewModels.Instructor;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Api.Controllers;

public class InstructorsController : BaseApiController
{
    private readonly IGenericRepository<Instructor> _instructorrepository;

    public InstructorsController(IGenericRepository<Instructor> Instructorrepository)
    {
        _instructorrepository = Instructorrepository;
    }


    [HttpGet]
    public ActionResult<IEnumerable<Instructor>> GetAllInstructors()
    {
        var Instructors = _instructorrepository.GetAll();

        return Ok(Instructors.ToViewModels());
    }

    [HttpGet("{id}")]
    public ActionResult<Instructor> GetInstructorById(int id)
    {
        var Instructor = _instructorrepository.GetByID(id);

        return Ok(Instructor.ToViewModel());
    }


    [HttpPost]
    public ActionResult<Instructor> AddInstructor(CreateInstructorViewModel viewModel)
    {
        var instructor = _instructorrepository.Add(new Instructor
        {
            Birthdate = viewModel.Birthdate,
            EnrollmentDate = viewModel.EnrollmentDate,
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName
        });

        _instructorrepository.SaveChanges();

        return Ok(instructor.ToViewModel());
    }

    [HttpPut("{id}")]
    public ActionResult<Instructor> UpdateInstructor(int id, CreateInstructorViewModel viewModel)
    {
        var instructorfromDb = _instructorrepository.GetByID(id);

        if (instructorfromDb != null)
        {
            var instructor = viewModel.ToInstructor();
            instructor.Id= id;

            _instructorrepository.Update(instructor);
            _instructorrepository.SaveChanges();
            return Ok(instructor.ToInstructorToReturnnDto());
        }
        return NotFound();

    }

    [HttpDelete]
    public ActionResult DeleteInstructor(int id)
    {
        _instructorrepository.Delete(id);
        _instructorrepository.SaveChanges();
        return Ok();
    }
}
