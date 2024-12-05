using CenterpriseAllProject.Models;
using CenterpriseAllProject.Security;
using CenterpriseAllProject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace CenterpriseAllProject.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger _logger;
        private readonly IDataProtector _protector;
        public HomeController(IEmployeeRepository employeeRepository, 
            IWebHostEnvironment hostingEnvironment, ILogger<HomeController> logger,IDataProtectionProvider protectionProvider
            ,DataProtectionPurposeStrings dataProtectionPurposeStrings)
        {
            _hostingEnvironment = hostingEnvironment;
            _employeeRepository = employeeRepository;
            _logger = logger;
            _protector = protectionProvider.CreateProtector(dataProtectionPurposeStrings.EmployeeIdRoutValue);
        }
        [AllowAnonymous]
         public ViewResult Index()
        {
            var employees = _employeeRepository.GetAllEmployee()
                .Select(e =>
                {
                    e.EncryptedId = _protector.Protect( e.Id.ToString());
                    return e;
                });
            return View(employees);
        }
        [AllowAnonymous]
        public ViewResult Details(string? encryptedId)
        {
            int? id = Int32.Parse( _protector.Unprotect( encryptedId! ));
            Employee model = _employeeRepository.GetEmployee(id.Value);
            if(model is null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id.Value);
            }
            HomeDetailsViewModel viewModel = new HomeDetailsViewModel()
            {
                Employee = model,
                PageTitle = "Employee Details"
            };
            return View(viewModel);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);
                Employee emp = new Employee { 
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    PhotoPath = uniqueFileName,
                } ;
                _employeeRepository.Add(emp);
                return RedirectToAction("Details", new { id = emp.Id });
            }
            return View();
        }
        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel model = new EmployeeEditViewModel()
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                Employee employee = _employeeRepository.GetEmployee(model.Id);
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Department = model.Department;
                if(model.Photo is not null)
                {
                    if(model.ExistingPhotoPath is not null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images",model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    employee.PhotoPath = ProcessUploadedFile(model);
                }
          
                _employeeRepository.Update(employee);
                return RedirectToAction("index");
            }
            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo is not null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string FilePath = Path.Combine(uploadsFolder, uniqueFileName);
                using(var fileStream = new FileStream(FilePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
                
            }

            return uniqueFileName;
        }
    }
}
