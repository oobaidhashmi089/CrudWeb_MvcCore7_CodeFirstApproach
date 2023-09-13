using CrudWebE.Data;
using CrudWebE.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;




namespace CrudWebE.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDBcontext employeeDBcontext;


        public EmployeeController(EmployeeDBcontext employeeDBcontext)
        {
            this.employeeDBcontext = employeeDBcontext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var employees = await employeeDBcontext.Employees.ToListAsync();


                return View(employees);
            }
            catch (Exception ex)
            {


                // Handle the exception gracefully or return an error response to the client
                // For example, you can return a custom error view or a specific error message.
                return View("Error"); // This assumes you have an "Error" view.
            }

        }

        [HttpGet]
        public IActionResult AddE()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddE(AddEmployees addEmployeesreq)
        {
            var employee = new Employeecs()
            {

                Name = addEmployeesreq.Name,
                Email = addEmployeesreq.Email,
                DateofBirth = addEmployeesreq.DateofBirth,
                Salary = addEmployeesreq.Salary,
                Department = addEmployeesreq.Department,
            };

            await employeeDBcontext.Employees.AddAsync(employee);
            await employeeDBcontext.SaveChangesAsync();

            return RedirectToAction("Index", "Employee");
        }
        [HttpGet]
        public async Task<IActionResult> View(int id)
        {
            //var employee = await employeeDBcontext.FirstOrDefaultAsync(e => e.EmpId == id);
            var employee = await employeeDBcontext.Employees.FirstOrDefaultAsync(x => x.EmpId == id);

            if (employee != null)
            {
                var updateEmployee = new UpdateEmployee()
                {
                    EmpId = employee.EmpId,
                    Name = employee.Name,
                    Email = employee.Email,
                    DateofBirth = employee.DateofBirth,
                    Salary = employee.Salary,
                    Department = employee.Department,

                };
                return await Task.Run(() =>
                
                     View("View",updateEmployee)
                );

            }
            return RedirectToAction("Index", "Employees");


        }



        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployee updateEmployee)
        {
            var employee = await employeeDBcontext.Employees.FindAsync(updateEmployee.EmpId);

            if (updateEmployee != null)
            {
                employee.Name = updateEmployee.Name;
                employee.Email = updateEmployee.Email;
                employee.DateofBirth = updateEmployee.DateofBirth;
                employee.Salary = updateEmployee.Salary;
                employee.Department = updateEmployee.Department;

                await employeeDBcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }


            // If ModelState is not valid, return to the edit view with validation errors
            return RedirectToAction("Index");




        }

        public async Task<IActionResult> Delete(UpdateEmployee updateEmployee) 
        {
            var employee = employeeDBcontext.Employees.FindAsync(updateEmployee.EmpId);

            if (employee != null) 
            {
                employeeDBcontext.Employees.Remove(await employee);
                await employeeDBcontext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }






        public IActionResult Edit() { return View(); }
    }
}
