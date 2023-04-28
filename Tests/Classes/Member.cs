namespace Tests.Classes;

public class Member
{
    public Guid MemberId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public byte Age { get; set; }

    public DateOnly BirthDate { get; set; }

    public bool IsActive { get; set; }

    public int[] FavoriteNumbers { get; set; }

    public DateTime LastLoginTimestamp { get; set; }
}
