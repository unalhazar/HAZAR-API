namespace Domain.Entities;

public class StudentLifestyle
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public double StudyHoursPerDay { get; set; }
    public double ExtracurricularHoursPerDay { get; set; }
    public double SleepHoursPerDay { get; set; }
    public double SocialHoursPerDay { get; set; }
    public double PhysicalActivityHoursPerDay { get; set; }
    public double GPA { get; set; }
    public string StressLevel { get; set; }
}


