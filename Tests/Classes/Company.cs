namespace Tests.Classes;

public class Company
{
    public Guid CompanyId { get; set; }
    public string CompanyName { get; set;}
    public string Address1 { get; set;}
    public int ZipCode { get; set;}
    public Member CEO { get; set; }
    public List<Department> Departments { get; set; }

}
