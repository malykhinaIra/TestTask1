using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestTask1.Business.Repositories;
using TestTask1.Web.ViewModels;

namespace TestTask1.Web.Controllers;

public class SalaryReportController : Controller
{
    readonly IEmployeeRepository _employees;
    readonly IDepartmentRepository _departments;
    readonly IPositionRepository _positions;

    readonly IMapper _mapper;

    public SalaryReportController(
        IEmployeeRepository employees, 
        IDepartmentRepository departments, 
        IPositionRepository positions, 
        IMapper mapper)
    {
        _employees = employees;
        _departments = departments;
        _positions = positions;
        
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var positions = await _positions.GetAllAsync();
        var departments = await _departments.GetAllAsync();

        ViewBag.Positions = _mapper.Map<PositionViewModel[]>(positions);
        ViewBag.Departments = _mapper.Map<DepartmentViewModel[]>(departments);
        
        var report = await _employees.GetSalaryReportAsync();

        return View(_mapper.Map<SalaryReportViewModel>(report));
    }

    [HttpGet]
    public async Task<IActionResult> Search(int? departmentIdentifier, int? positionIdentifier)
    {
        var positions = await _positions.GetAllAsync();
        var departments = await _departments.GetAllAsync();

        ViewBag.Positions = _mapper.Map<PositionViewModel[]>(positions);
        ViewBag.Departments = _mapper.Map<DepartmentViewModel[]>(departments);
        
        var report = await _employees.GetSalaryReportAsync(departmentIdentifier, positionIdentifier);
        var result = _mapper.Map<SalaryReportViewModel>(report);

        return Json(new { ok = true, data = result });
    }
    
    [HttpGet]
    public Task<IActionResult> ExportToTxt([FromQuery] SalaryReportViewModel report)
    {
        var content = GenerateReportText(report);
        var byteArray = Encoding.UTF8.GetBytes(content);
        var stream = new MemoryStream(byteArray);

        return Task.FromResult<IActionResult>(
            File(stream, "text/plain", $"salary-report-{DateTime.UtcNow}.txt"));
    }

    string GenerateReportText(SalaryReportViewModel report)
    {
        var text = new StringBuilder();

        text.AppendLine("Звіт по окладам");
        
        text.AppendLine($"Загальна сума окладів: {report.TotalSalary:C}");
        text.AppendLine($"Середній оклад: {report.AverageSalary:C}");
        text.AppendLine($"Максимальний оклад: {report.MaxSalary:C}");
        text.AppendLine($"Мінімальний оклад: {report.MinSalary:C}");

        return text.ToString();
    }
}