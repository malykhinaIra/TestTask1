using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestTask1.Business.Repositories;
using TestTask1.Web.ViewModels;

namespace TestTask1.Web.Controllers;

public class PositionController : Controller
{
    readonly IPositionRepository _positions;

    readonly IMapper _mapper;
    
    public PositionController(IPositionRepository positions, IMapper mapper)
    {
        _positions = positions;
        
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetByDepartment(int departmentId)
    {
        var positions = await _positions.GetManyByDepartmentAsync(departmentId);
        
        return Json(new { ok = true, data = _mapper.Map<PositionViewModel[]>(positions) });
    }
}