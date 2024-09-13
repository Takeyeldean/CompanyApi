using Employee.Data;
using Employee.Data.Entites;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controller
{

    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
        {
        
                private AppDbContext _dbcontext;
                public EmployeeController(AppDbContext dbcontext)
                {
                    _dbcontext = dbcontext;
                }

        [HttpPost]
        [Route("Add")]
        public ActionResult<Employees> AddEmployee(CEmployee employee)
        {
 
                var NewEmployee = new Employees
                {
                    Emp_Id = 0,
                    FDep_Id = employee.Dep_Id,
                    Name = employee.Name,
                    Salary = employee.Salary,
                    BirthDate = employee.BirthDate,
                };

                _dbcontext.Set<Employees>().Add(NewEmployee);
                _dbcontext.SaveChanges();
                return Ok(NewEmployee);
        } //Done

        [HttpGet]
        [Route("Get")]
        public ActionResult<Employees> Get(int id)
        {
            var record = _dbcontext.Set<Employees>().Find(id);
            if (record == null ) 
            { 
                Console.WriteLine("NOT FOUND");
                return NotFound();
            }
            if (record.IsDeleted)
            {
                Console.WriteLine("You deleted this employee");
                return NotFound();
            }
            
            return Ok(record);
        } //Done

        [HttpPut]
        [Route("Update")]
        public ActionResult<Employees> Update(CEmployee employee,int id) 
        {   
            var existingemployee = _dbcontext.Set<Employees>().Find(id);
            if (existingemployee != null) {
                existingemployee.Name = employee.Name;
                existingemployee.FDep_Id = employee.Dep_Id;
                existingemployee.Salary = employee.Salary;
                existingemployee.BirthDate = employee.BirthDate;
            _dbcontext.Set<Employees>().Update(existingemployee);
                _dbcontext.SaveChanges();
            return Ok();
            }else return NotFound();
        }//Done

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(int id)
        {
            var record = _dbcontext.Set<Employees>().Find(id);
            if (record != null) { 
            _dbcontext.Set<Employees>().Remove(record);
                _dbcontext.SaveChanges();
                return Ok();
            }
            return NotFound();
        } //Done
       


    }
    }
