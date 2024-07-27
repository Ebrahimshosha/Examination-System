using ExaminationSystem.Api.Data;
using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.ViewModels.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExaminationSystem.Api.Controllers;

public class CoursesController : BaseApiController
{
    private readonly IGenericRepository<Course> _courserepository;

    public CoursesController(IGenericRepository<Course> Courserepository)
    {
        _courserepository = Courserepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Course>> GetAllCourses( )
    {
        var Courses = _courserepository.GetAll();
        return Ok( Courses );

    }

    [HttpGet("{id}")]
    public ActionResult<Course> GetCourseById(int id)
    {
        var course = _courserepository.GetByID(id);
        return Ok( course );    
    }

    [HttpPost]
    public ActionResult<Course> AddCourse(CreateCourseViewModel model)
    {
       var course = _courserepository.Add(new Course
        {
            CreditHours = model.CreditHours,
            InstructorId = model.InstructorId,
            Name = model.Name
            
        });
        _courserepository.SaveChanges();
        return Ok(course);
    }

    [HttpPut]
    public ActionResult<Course> UpdateCourse(int id, CreateCourseViewModel model)
    {
        var course = _courserepository.GetByID(id);

        course.Id = id;
        course.Name = model.Name;
        course.CreditHours = model.CreditHours;
        course.InstructorId = model.InstructorId;

        _courserepository.Update(course);
        _courserepository.SaveChanges();

        return Ok(course);
    }

    [HttpDelete]
    public ActionResult DeleteCourse(int id) 
    {
        _courserepository.Delete(id);
        _courserepository.SaveChanges();
        return Ok();
    }
}
