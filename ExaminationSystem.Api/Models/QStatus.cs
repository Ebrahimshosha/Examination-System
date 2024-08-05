using System.Runtime.Serialization;

namespace ExaminationSystem.Api.Models;

public enum QStatus
{
    [EnumMember(Value = "simple")]
    simple,

    [EnumMember(Value = "medium")]
    medium,

    [EnumMember(Value = "hard")]
    hard
}
