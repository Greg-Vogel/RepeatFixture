
namespace Tests.Classes;

public class Department
{
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; }
    public short BuildingFloor { get; set; }

    public List<Member> Employees { get; set; }
}
