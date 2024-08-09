﻿
namespace ExaminationSystem.Api.DTO.Exam;

public class ExamToReturnDto
{
    public int Id { get; set; }

    public DateTime StartDate { get; set; }

    public int TotalGrade { get; set; }

    public int InstructorId { get; set; }

    public int CourseId { get; set; }

    public string ExamStatus { get; set; }


}
