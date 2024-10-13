using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestTask1.Business.Repositories;
using TestTask1.Web.ViewModels;

namespace TestTask1.Web.Controllers;

public class DepartmentController : Controller
{
    readonly IDepartmentRepository _departments;

    readonly IMapper _mapper;
    
    public DepartmentController(IDepartmentRepository departments, IMapper mapper)
    {
        _departments = departments;
        
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetByPosition(int positionId)
    {
        var departments = await _departments.GetManyByPositionAsync(positionId);
        
        return Json(new { ok = true, data = _mapper.Map<DepartmentViewModel[]>(departments) });
    }
}