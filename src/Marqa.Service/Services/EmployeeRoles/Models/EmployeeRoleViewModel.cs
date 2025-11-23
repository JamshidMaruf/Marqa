namespace Marqa.Service.Services.EmployeeRoles.Models;

public class EmployeeRoleViewModel
{
    public int Id {  get; set; }
    public string Name { get; set; }
    public CompanyInfo Company { get; set; }

    public class CompanyInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}