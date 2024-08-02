﻿using ExaminationSystem.Api.DTO.Exam;
using ExaminationSystem.Api.Models;
using ExaminationSystem.Api.ViewModels.Exam;

namespace ExaminationSystem.Api.Services.ExamService;

public interface IExamService
{
    ExamToReturnDto CreateExamService(ExamDto model);
     Exam TakeExam(int studentId, int Courseid, string examStatus);
}
