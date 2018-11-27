using Microsoft.AspNetCore.Mvc;
using System.Linq;
using AspCoreCRUD.Data;
using AspCoreCRUD.Models;
using System.Threading.Tasks;

namespace AspCoreCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public EmployeeController(ApplicationDbContext db)
        {
            _db = db;
        }

        // Action Methods

        [HttpGet]
        public IActionResult GetEmployees() 
        {
            return Ok(_db.Employees.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee objEmployee) 
        {
            if (!ModelState.IsValid)
            {
                return new JsonResult("Error While Creating New Employee");
            }
            _db.Employees.Add(objEmployee);
            await _db.SaveChangesAsync();

            return new JsonResult("Employee Created Successfully");

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] int id, [FromBody] Employee objEmployee) 
        {
            if (objEmployee == null || id != objEmployee.id)
            {
                return new JsonResult("Employee Was Not Found");
            }
            else
            {
                _db.Employees.Update(objEmployee);
                await _db.SaveChangesAsync();
                return new JsonResult("Employee Was Updated Successfully");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id) 
        {
            var findEmployee = await _db.Employees.FindAsync(id);
            if (findEmployee == null)
            {
                return NotFound();
            }
            else
            {
                _db.Employees.Remove(findEmployee);
               await _db.SaveChangesAsync();
                return new JsonResult("Employee Was Deleted Successfully");
            }
        }
    }
}
