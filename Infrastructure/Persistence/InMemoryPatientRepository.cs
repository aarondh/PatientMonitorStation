using WPFTest.Domain.Entities;
using WPFTest.Domain.Ports;

namespace WPFTest.Infrastructure.Persistence;

public class InMemoryPatientRepository : IPatientRepository
{
    private readonly List<Patient> _patients = new()
    {
        // ICU 101 - 4 patients
        new Patient("patient-1", "John", "Smith", new DateTime(1965, 3, 15), "Dr. Anderson", "room-1"),
        new Patient("patient-2", "Jane", "Doe", new DateTime(1978, 7, 22), "Dr. Martinez", "room-1"),
        new Patient("patient-3", "Bob", "Johnson", new DateTime(1982, 11, 8), "Dr. Thompson", "room-1"),
        new Patient("patient-4", "Alice", "Williams", new DateTime(1970, 5, 30), "Dr. Anderson", "room-1"),

        // ICU 102 - 4 patients
        new Patient("patient-5", "Michael", "Brown", new DateTime(1955, 9, 12), "Dr. Chen", "room-2"),
        new Patient("patient-6", "Sarah", "Davis", new DateTime(1990, 2, 18), "Dr. Martinez", "room-2"),
        new Patient("patient-7", "David", "Miller", new DateTime(1968, 4, 25), "Dr. Thompson", "room-2"),
        new Patient("patient-8", "Emma", "Wilson", new DateTime(1985, 8, 14), "Dr. Anderson", "room-2"),

        // ICU 103 - 4 patients
        new Patient("patient-9", "James", "Moore", new DateTime(1972, 12, 3), "Dr. Chen", "room-3"),
        new Patient("patient-10", "Olivia", "Taylor", new DateTime(1980, 6, 19), "Dr. Martinez", "room-3"),
        new Patient("patient-11", "William", "Anderson", new DateTime(1963, 10, 7), "Dr. Thompson", "room-3"),
        new Patient("patient-12", "Sophia", "Thomas", new DateTime(1988, 1, 28), "Dr. Anderson", "room-3"),

        // ICU 104 - 4 patients
        new Patient("patient-13", "Benjamin", "Jackson", new DateTime(1975, 5, 11), "Dr. Chen", "room-4"),
        new Patient("patient-14", "Isabella", "White", new DateTime(1992, 9, 16), "Dr. Martinez", "room-4"),
        new Patient("patient-15", "Lucas", "Harris", new DateTime(1958, 3, 22), "Dr. Thompson", "room-4"),
        new Patient("patient-16", "Mia", "Martin", new DateTime(1983, 7, 9), "Dr. Anderson", "room-4"),

        // ICU 105 - 4 patients
        new Patient("patient-17", "Henry", "Garcia", new DateTime(1969, 11, 30), "Dr. Chen", "room-5"),
        new Patient("patient-18", "Charlotte", "Rodriguez", new DateTime(1987, 2, 5), "Dr. Martinez", "room-5"),
        new Patient("patient-19", "Alexander", "Martinez", new DateTime(1961, 6, 18), "Dr. Thompson", "room-5"),
        new Patient("patient-20", "Amelia", "Robinson", new DateTime(1979, 10, 24), "Dr. Anderson", "room-5"),

        // ICU 106 - 4 patients
        new Patient("patient-21", "Daniel", "Clark", new DateTime(1973, 4, 12), "Dr. Chen", "room-6"),
        new Patient("patient-22", "Harper", "Lewis", new DateTime(1991, 8, 7), "Dr. Martinez", "room-6"),
        new Patient("patient-23", "Matthew", "Lee", new DateTime(1966, 12, 15), "Dr. Thompson", "room-6"),
        new Patient("patient-24", "Evelyn", "Walker", new DateTime(1984, 3, 29), "Dr. Anderson", "room-6"),

        // Ward 201 - 4 patients
        new Patient("patient-25", "Joseph", "Hall", new DateTime(1977, 7, 21), "Dr. Chen", "room-7"),
        new Patient("patient-26", "Abigail", "Allen", new DateTime(1989, 11, 4), "Dr. Martinez", "room-7"),
        new Patient("patient-27", "Samuel", "Young", new DateTime(1964, 5, 17), "Dr. Thompson", "room-7"),
        new Patient("patient-28", "Emily", "King", new DateTime(1981, 9, 26), "Dr. Anderson", "room-7"),

        // Ward 202 - 4 patients
        new Patient("patient-29", "David", "Wright", new DateTime(1971, 1, 8), "Dr. Chen", "room-8"),
        new Patient("patient-30", "Elizabeth", "Lopez", new DateTime(1993, 5, 13), "Dr. Martinez", "room-8"),
        new Patient("patient-31", "Jackson", "Hill", new DateTime(1959, 9, 20), "Dr. Thompson", "room-8"),
        new Patient("patient-32", "Sofia", "Scott", new DateTime(1986, 2, 2), "Dr. Anderson", "room-8"),

        // Ward 203 - 4 patients
        new Patient("patient-33", "Sebastian", "Green", new DateTime(1974, 6, 14), "Dr. Chen", "room-9"),
        new Patient("patient-34", "Avery", "Adams", new DateTime(1990, 10, 19), "Dr. Martinez", "room-9"),
        new Patient("patient-35", "Jack", "Baker", new DateTime(1962, 2, 27), "Dr. Thompson", "room-9"),
        new Patient("patient-36", "Scarlett", "Nelson", new DateTime(1988, 7, 6), "Dr. Anderson", "room-9"),

        // Ward 204 - 4 patients
        new Patient("patient-37", "Owen", "Carter", new DateTime(1976, 11, 9), "Dr. Chen", "room-10"),
        new Patient("patient-38", "Victoria", "Mitchell", new DateTime(1985, 3, 16), "Dr. Martinez", "room-10"),
        new Patient("patient-39", "Wyatt", "Perez", new DateTime(1967, 7, 23), "Dr. Thompson", "room-10"),
        new Patient("patient-40", "Grace", "Roberts", new DateTime(1992, 12, 1), "Dr. Anderson", "room-10"),

        // Ward 205 - 4 patients
        new Patient("patient-41", "Luke", "Turner", new DateTime(1980, 4, 10), "Dr. Chen", "room-11"),
        new Patient("patient-42", "Chloe", "Phillips", new DateTime(1987, 8, 18), "Dr. Martinez", "room-11"),
        new Patient("patient-43", "Isaac", "Campbell", new DateTime(1963, 1, 25), "Dr. Thompson", "room-11"),
        new Patient("patient-44", "Lily", "Parker", new DateTime(1991, 5, 5), "Dr. Anderson", "room-11"),

        // Ward 206 - 4 patients
        new Patient("patient-45", "Ryan", "Evans", new DateTime(1978, 9, 12), "Dr. Chen", "room-12"),
        new Patient("patient-46", "Zoey", "Edwards", new DateTime(1986, 2, 20), "Dr. Martinez", "room-12"),
        new Patient("patient-47", "Nathan", "Collins", new DateTime(1960, 6, 28), "Dr. Thompson", "room-12"),
        new Patient("patient-48", "Hannah", "Stewart", new DateTime(1994, 10, 3), "Dr. Anderson", "room-12")
    };

    public Task<IEnumerable<Patient>> GetAllPatientsAsync()
    {
        return Task.FromResult<IEnumerable<Patient>>(_patients);
    }

    public Task<Patient?> GetPatientByIdAsync(string patientId)
    {
        var patient = _patients.FirstOrDefault(p => p.Id == patientId);
        return Task.FromResult(patient);
    }

    public Task<IEnumerable<Patient>> GetPatientsByRoomIdAsync(string roomId)
    {
        var patients = _patients.Where(p => p.RoomId == roomId);
        return Task.FromResult<IEnumerable<Patient>>(patients);
    }
}
