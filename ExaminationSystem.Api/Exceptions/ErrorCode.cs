namespace ExaminationSystem.Api.Exceptions;


public enum ErrorCode
{
    None = 0,
    UnKnown = 1,
    StudentHasNotRegisteredForThisCourse = 1000,
    NotallowedToTakeSameExamAnotherTime = 2000,
    InstructordidntGivethatCourse = 3000
}

