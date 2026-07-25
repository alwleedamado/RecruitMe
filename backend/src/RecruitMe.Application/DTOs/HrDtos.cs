namespace RecruitMe.Application.DTOs;

public record CreateHr(string FullName, string Email,decimal Salary, DateTime HireDate, string Password);

public record HrDto(int Id, string FullName, string Email, DateTime HireDate, decimal Salary);

public record UpdateHr(int Id, string FullName, string Email);