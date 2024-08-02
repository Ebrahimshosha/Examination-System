using System.Runtime.Serialization;

namespace ExaminationSystem.Api.Models;

public enum ExamStatus
{
    [EnumMember(Value = "final")]
    final,

    [EnumMember(Value = "Quiz")]
    Quiz
}
