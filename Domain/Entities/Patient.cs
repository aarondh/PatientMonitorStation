namespace WPFTest.Domain.Entities;

public class Patient
{
    public string Id { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public string PrimaryCareGiver { get; init; } = string.Empty;
    public string RoomId { get; set; } = string.Empty;

    public Patient(string id, string firstName, string lastName, DateTime dateOfBirth, string primaryCareGiver, string roomId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        PrimaryCareGiver = primaryCareGiver;
        RoomId = roomId;
    }

    public string FullName => $"{FirstName} {LastName}";

    public int Age
    {
        get
        {
            var today = DateTime.Today;
            var age = today.Year - DateOfBirth.Year;
            if (DateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}
