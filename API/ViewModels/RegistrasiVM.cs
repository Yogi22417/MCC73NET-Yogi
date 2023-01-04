namespace API.Models;

public class RegistrasiVM
{
    public string? NIK { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Phone { get; set; }
    public DateTime BirthDate { get; set; }
    public int Salary { get; set; }
    public string? Email { get; set; }
    public Gender Gender { get; set; }
    public string? Password { get; set; }
    public string? GPA { get; set; }
    public string? Degree { get; set; }
    public string? UniversityName { get; set; }
}