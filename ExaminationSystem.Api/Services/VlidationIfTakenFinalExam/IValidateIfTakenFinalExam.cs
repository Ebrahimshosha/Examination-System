using ExaminationSystem.Api.Interfaces;
using ExaminationSystem.Api.Models;

namespace ExaminationSystem.Api.Services;

public interface IValidateIfTakenFinalExam
{
    bool HasTakenFinalExam(int studentId, int Courseid, ExamStatus status);
}
