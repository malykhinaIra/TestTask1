using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestTask1.Business.Storages;
using TestTask1.Business.ValueObjects;
using TestTask1.Web.ViewModels;

namespace TestTask1.Web.Controllers;

public class HomeController : Controller
{
    readonly ICompanyInfoStorage _companyInfo;

    readonly IMapper _mapper;

    public HomeController(ICompanyInfoStorage companyInfo, IMapper mapper)
    {
        _companyInfo = companyInfo;
        
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var companyInfo = _companyInfo.GetCompanyInfo();
        
        return View(_mapper.Map<CompanyInfo, CompanyInfoViewModel>(companyInfo));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}