
namespace ExaminationSystem.Api.Controllers;

public class CoursesController : BaseApiController
{
    private readonly IGenericRepository<Course> _courserepository;
    private readonly IStudentCourseService _studentCourseService;
    private readonly IMapper _mapper;

    public CoursesController(IGenericRepository<Course> Courserepository,
        IStudentCourseService studentCourseService,
        IMapper mapper)
    {
        _courserepository = Courserepository;
        _studentCourseService = studentCourseService;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<CourseToReturnDto>> GetAllCourses()
    {
        var courses = _courserepository.GetAll();
        var coursesToReturnDto = _mapper.ProjectTo<CourseToReturnDto>(courses);
        return Ok(coursesToReturnDto);

    }

    [HttpGet("{id}")]
    public ActionResult<CourseToReturnDto> GetCourseById(int id)
    {
        var course = _courserepository.GetByID(id);

        var CourseToReturnDto = _mapper.Map<CourseToReturnDto>(course);
        return Ok(CourseToReturnDto);
    }

    [HttpPost]
    public ActionResult<CourseToReturnDto> AddCourse(CreateCourseViewModel model)
    {
        var course = _courserepository.Add(new Course
        {
            CreditHours = model.CreditHours,
            InstructorId = model.InstructorId,
            Name = model.Name

        });
        _courserepository.SaveChanges();

        var CourseToReturnDto = _mapper.Map<CourseToReturnDto>(course);
        return Ok(CourseToReturnDto);
    }

    [HttpPut]
    public ActionResult<CourseToReturnDto> UpdateCourse(int id, CreateCourseViewModel model)
    {
        var course = _courserepository.GetByID(id);

        course.Id = id;
        course.Name = model.Name;
        course.CreditHours = model.CreditHours;
        course.InstructorId = model.InstructorId;

        _courserepository.Update(course);
        _courserepository.SaveChanges();

        var CourseToReturnDto = _mapper.Map<CourseToReturnDto>(course);
        return Ok(CourseToReturnDto);
    }

    [HttpDelete]
    public ActionResult DeleteCourse(int id)
    {
        _courserepository.Delete(id);
        _courserepository.SaveChanges();
        return Ok();
    }

    [HttpPost]
    public ActionResult<StudentCourse> EnrollToCourse(int studentId, int courseId)
    {
        var studentCourse = _studentCourseService.EnrollCourseToStudent(studentId, courseId);
        return Ok(studentCourse);
    }
}
