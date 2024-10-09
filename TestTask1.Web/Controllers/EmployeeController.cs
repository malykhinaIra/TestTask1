using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestTask1.Business.Entities;
using TestTask1.Business.Repositories;
using TestTask1.Web.ViewModels;

namespace TestTask1.Web.Controllers;

public class EmployeeController : Controller
{
    readonly IEmployeeRepository _employees;
    readonly IPositionRepository _positions;
    readonly IDepartmentRepository _departments;

    readonly IMapper _mapper;
    
    public EmployeeController(
        IEmployeeRepository employees, 
        IPositionRepository positions, 
        IDepartmentRepository departments, 
        IMapper mapper)
    {
        _employees = employees;
        _positions = positions;
        _departments = departments;
        
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var positions = await _positions.GetAllAsync();
        var departments = await _departments.GetAllAsync();
        var employees = await _employees.GetAllAsync();

        ViewBag.Positions = _mapper.Map<PositionViewModel[]>(positions);
        ViewBag.Departments = _mapper.Map<DepartmentViewModel[]>(departments);
        
        return View(_mapper.Map<EmployeeViewModel[]>(employees));
    }

    [HttpGet]
    public async Task<IActionResult> Search(int? departmentIdentifier, int? positionIdentifier, string? query)
    {
        var employees = await _employees.GetManyAsync(departmentIdentifier, positionIdentifier, query);
        
        return Json(new { ok = true, data = _mapper.Map<EmployeeViewModel[]>(employees) });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var employee = await _employees.GetOneAsync(id);
        var positions = await _positions.GetAllAsync();
        var departments = await _departments.GetAllAsync();

        ViewBag.Positions = _mapper.Map<PositionViewModel[]>(positions);
        ViewBag.Departments = _mapper.Map<DepartmentViewModel[]>(departments);
        
        return View(_mapper.Map<EmployeeViewModel>(employee));
    }

    [HttpPut]
    public async Task<IActionResult> Edit(EmployeeViewModel updatedModel)
    {
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(
            updatedModel,
            new ValidationContext(updatedModel),
            validationResults,
            true);

        if (!isValid)
            return Json(new { ok = false, validation = validationResults.Select(result => result.ErrorMessage) });

        var employee = _mapper.Map<Employee>(updatedModel);
        await _employees.UpdateAsync(employee);

        return Json(new { ok = true });
    }
}