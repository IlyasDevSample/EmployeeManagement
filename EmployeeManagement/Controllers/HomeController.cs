using EmployeeManagement.Models;
using EmployeeManagement.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    
    public class HomeController : Controller
    {
        IEmployeeRepository employeeRepository;
        private readonly IWebHostEnvironment hostEnvironment;
        private readonly ILogger<HomeController> logger;

        public HomeController(IEmployeeRepository employeeRepository,
            IWebHostEnvironment hostEnvironment,
            ILogger<HomeController> _logger)
        {
            this.logger = _logger;
            this.employeeRepository = employeeRepository;
            this.hostEnvironment = hostEnvironment;
            
        }
        [HttpGet]
        [AllowAnonymous]
        public ViewResult Index()
        {
            var model = employeeRepository.GetAllEmployee();
            return View(model);
        }
        [AllowAnonymous]
        public ViewResult Details(int id)
        {
            //throw new Exception("Error in Details");

            logger.LogTrace("Trace Log");
            logger.LogDebug("Debug Log");
            logger.LogInformation("Information Log");
            logger.LogWarning("Warning Log");
            logger.LogError("Error Log");
            logger.LogCritical("Critical Log");


            Employee employee = employeeRepository.GetEmployee(id);
            if (employee is null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };

            return View(homeDetailsViewModel);
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
                String uniqueFileName = null;
                //More than One file Upload

                //if (model.Photos != null && model.Photos.Count > 0)
                //{
                //    foreach (var file in model.Photos)
                //    {
                //        string UplaodFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                //        uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                //        string filePath = Path.Combine(UplaodFolder, uniqueFileName);
                //        file.CopyTo(new FileStream(filePath, FileMode.Create));

                //    }

                //}

                //One file Upload
                if (model.Photo != null)
                {
                    string UplaodFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                    string filePath = Path.Combine(UplaodFolder, uniqueFileName);
                    model.Photo.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                Employee newEmployee = new Employee()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Departement = model.Departement,
                    PhotoPath = uniqueFileName
                };
                employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }

        [HttpGet]
        public ViewResult Edit(int id)
        {
            Employee employee = employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel();
            employeeEditViewModel.Id = id;
            employeeEditViewModel.Name = employee.Name;
            employeeEditViewModel.Email = employee.Email;
            employeeEditViewModel.Departement = employee.Departement;
            employeeEditViewModel.ExistingPhotoPath = employee.PhotoPath;
            
            
           
            return View(employeeEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            // Check if the provided data is valid, if not rerender the edit view
            // so the user can correct and resubmit the edit form
            if (ModelState.IsValid)
            {
                // Retrieve the employee being edited from the database
                Employee employee = employeeRepository.GetEmployee(model.Id);
                // Update the employee object with the data in the model object
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Departement = model.Departement;

                // If the user wants to change the photo, a new photo will be
                // uploaded and the Photo property on the model object receives
                // the uploaded photo. If the Photo property is null, user did
                // not upload a new photo and keeps his existing photo
                if (model.Photo != null)
                {
                    // If a new photo is uploaded, the existing photo must be
                    // deleted. So check if there is an existing photo and delete
                    if (model.ExistingPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostEnvironment.WebRootPath,
                            "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    // Save the new photo in wwwroot/images folder and update
                    // PhotoPath property of the employee object which will be
                    // eventually saved in the database
                    employee.PhotoPath = ProcessUploadedFile(model);
                }

                // Call update method on the repository service passing it the
                // employee object to update the data in the database table
                Employee updatedEmployee = employeeRepository.Update(employee);

                return RedirectToAction("index");
            }

            return View(model);
        }
        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
            string uniqueFileName = null;

            if (model.Photo != null)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }





















    }

}
