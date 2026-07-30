namespace RecruitMe.Application.DTOs;

public class RegisterHrRequest(string fullName, string email,decimal salary, DateTime hireDate, string password)
{
    public string FullName { get; init; } = fullName;
    public string Email { get; init; } = email;
    public decimal Salary { get; init; } = salary;
    public DateTime HireDate { get; init; } = hireDate;
    public string Password { get; init; } = password;

    public void Deconstruct(out string fullName, out string email, out decimal salary, out DateTime hireDate, out string password)
    {
        fullName = this.FullName;
        email = this.Email;
        salary = this.Salary;
        hireDate = this.HireDate;
        password = this.Password;
    }
}

public class HrDto
{
    protected HrDto()
    {
    }

    public HrDto(int id, string fullName, string email, DateTime hireDate, decimal salary)
    {
        Id = id;
        FullName = fullName;
        Email = email;
        HireDate = hireDate;
        Salary = salary;
    }

    public int Id { get; init; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime HireDate { get; init; }
    public decimal Salary { get; init; }

    public void Deconstruct(out int id, out string fullName, out string email, out DateTime hireDate, out decimal salary)
    {
        id = this.Id;
        fullName = this.FullName;
        email = this.Email;
        hireDate = this.HireDate;
        salary = this.Salary;
    }
}

public class UpdateHr(int id, string fullName, string email)
{
    public int Id { get; init; } = id;
    public string FullName { get; init; } = fullName;
    public string Email { get; init; } = email;

    public void Deconstruct(out int id, out string fullName, out string email)
    {
        id = this.Id;
        fullName = this.FullName;
        email = this.Email;
    }
}
