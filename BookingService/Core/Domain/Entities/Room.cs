namespace Domain.Entities;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Level { get; set; }
    public string Email { get; set; }
    public bool InMaintenance { get; set; }

    public bool IsAvaible
    {
        get
        {
            return !(InMaintenance || HasGuest);
        }
    }

    public bool HasGuest
    {
        get { return true; }
    }
}
