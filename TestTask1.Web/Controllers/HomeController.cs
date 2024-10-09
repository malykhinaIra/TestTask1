using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TestTask1.Business.Entities;
using TestTask1.Business.Repositories;
using TestTask1.Web.ViewModels;

namespace TestTask1.Web.Controllers;

public class HomeController : Controller
{
    readonly ICompanyInfoRepository _companyInfo;

    readonly IMapper _mapper;

    public HomeController(ICompanyInfoRepository companyInfo, IMapper mapper)
    {
        _companyInfo = companyInfo;
        
        _mapper = mapper;
    }

    public IActionResult Index()
    {
        var companyInfo = _companyInfo.GetOneAsync();
        
        return View(_mapper.Map<CompanyInfo, CompanyInfoViewModel>(companyInfo));
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}