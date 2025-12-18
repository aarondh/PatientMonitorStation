namespace WPFTest.Domain.Entities;

public class Room
{
    public string Id { get; init; }
    public string Name { get; init; }

    public Room(string id, string name)
    {
        Id = id;
        Name = name;
    }
}
