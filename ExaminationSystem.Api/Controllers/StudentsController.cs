
namespace ExaminationSystem.Api.Controllers;

public class StudentsController : BaseApiController
{
    private readonly IGenericRepository<Student> _studentRepository;
    private readonly IMapper _mapper;

    public StudentsController(IGenericRepository<Student> StudentRepository,
         IMapper mapper)
    {
        _studentRepository = StudentRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<StudentToReturnDto>> GetAllCourses()
    {
        var students = _studentRepository.GetAll();

        var studentesToReturnDto = _mapper.ProjectTo<StudentToReturnDto>(students);
        return Ok(studentesToReturnDto);
    }

    [HttpGet("{id}")]
    public ActionResult<StudentToReturnDto> GetCourseById(int id)
    {
        var student = _studentRepository.Get(x => x.Id == id);

        var studenteToReturnDto = _mapper.ProjectTo<StudentToReturnDto>(student).FirstOrDefault();
        return Ok(studenteToReturnDto);
    }

    [HttpPost]
    public ActionResult<StudentToReturnDto> AddStudent(CreateStudentViewModel model)
    {
        var student = _studentRepository.Add(new Student
        {
            FirstName = model.FirstName,
            LastName = model.LastName

        });
        _studentRepository.SaveChanges();

        var studenteToReturnDto = _mapper.Map<StudentToReturnDto>(student);
        return Ok(studenteToReturnDto);
    }

    [HttpPut]
    public ActionResult<StudentToReturnDto> UpdateCourse(int id, StudentToReturnDto model)
    {
        var student = _studentRepository.GetByID(id);

        student.FirstName = model.FirstName;
        student.LastName = model.LastName;

        _studentRepository.Update(student);
        _studentRepository.SaveChanges();

        var studentToReturnDto = _mapper.Map<StudentToReturnDto>(student);
        return Ok(studentToReturnDto);
    }

    [HttpDelete]
    public ActionResult DeleteCourse(int id)
    {
        _studentRepository.Delete(id);
        _studentRepository.SaveChanges();
        return Ok();
    }
}
