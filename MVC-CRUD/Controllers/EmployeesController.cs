using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_CRUD.Data;
using MVC_CRUD.Models;
using MVC_CRUD.Models.Domains;

namespace MVC_CRUD.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly MVCDbContext mvcDbContext;

        public EmployeesController(MVCDbContext mvcDbContext)
        {
            this.mvcDbContext = mvcDbContext;
        }

        // Get All Employees
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await mvcDbContext.Employees.ToListAsync();

            return View(employees);
        }

        // Add New Employees
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        // Add Data to Database
        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
            var employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                Department = addEmployeeRequest.Department,
                DateofBirth = addEmployeeRequest.DateofBirth,

            };

            await mvcDbContext.Employees.AddAsync(employee);
            await mvcDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // View Individual Employee
        [HttpGet]
        public async Task <IActionResult> View(Guid id)
        {
            var employee = await mvcDbContext.Employees.FirstOrDefaultAsync(x => x.Id == id);

            if (employee != null) {

                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = Guid.NewGuid(),
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateofBirth = employee.DateofBirth,
                };

                return await Task.Run(()=> View("Update", viewModel));
            }
            return RedirectToAction("Index");
        }

        // Update Employees
        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDbContext.Employees.FindAsync(model.Id);

            if(employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.Department = model.Department;
                employee.DateofBirth = model.DateofBirth;


                await mvcDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
          
            }
            return RedirectToAction("Index");
        }

        // Delete Employees
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDbContext.Employees.FindAsync(model.Id);
            if(employee != null)
            {
                mvcDbContext.Employees.Remove(employee);
                await mvcDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
