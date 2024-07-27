using ExaminationSystem.Api.DTO;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.ViewModels.Instructor;

namespace ExaminationSystem.Api.Extentions;

public static class InstructorViewModelExtensions
{
    public static CreateInstructorViewModel ToViewModel(this Instructor instructor)
    {
        return new CreateInstructorViewModel
        {
            FirstName = instructor.FirstName,
            LastName = instructor.LastName,
        };
    }

    public static IEnumerable<CreateInstructorViewModel> ToViewModels(this IQueryable<Instructor> instructors)
    {
        return instructors.Select(x => x.ToViewModel())
            .ToList();
    }

    public static InstructorToReturnnDto ToInstructorToReturnnDto(this Instructor instructor)
    {
        return new InstructorToReturnnDto()
        {
            Id = instructor.Id,
            FirstName = instructor.FirstName,
            LastName = instructor.LastName
        };
    }
    public static Instructor ToInstructor(this CreateInstructorViewModel InstructorViewModel)
    {
        return new Instructor()
        {
            FirstName = InstructorViewModel.FirstName,
            LastName = InstructorViewModel.LastName
        };
    }
}
