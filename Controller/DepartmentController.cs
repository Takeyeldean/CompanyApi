using Employee.Data.Entites;
using Employee.Data;
using Microsoft.AspNetCore.Mvc;

namespace Employee.Controller
{

    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private AppDbContext _dbcontext;
        public DepartmentController(AppDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        [HttpPost]
        [Route("Add")]
        public ActionResult<Department> AddEmployee(CDepartment department)
        {

            var NewDepartment = new Department
            {
                DepName = department.DepName,
                Dep_Id = department.Dep_Id,
                
            };

            _dbcontext.Set<Department>().Add(NewDepartment);
            _dbcontext.SaveChanges();
            return Ok(NewDepartment);
        } //Done

        [HttpGet]
        [Route("Get")]
        public ActionResult<Department> Get(int id)
        {
            var record = _dbcontext.Set<Department>().Find(id);
            if (record == null)
            {
                Console.WriteLine("NOT FOUND");
                return NotFound();
            }
            if(record.IsDeleted)
            {
                Console.WriteLine("You deleted this department");
                return NotFound();
            }
            return Ok(record);
        }//Done

        [HttpPut]
        [Route("Update")]
        public ActionResult Update(CDepartment department,int id)
        {
            var existingemployee = _dbcontext.Set<Department>().Find(id);
            if (existingemployee != null)
            {
              
               existingemployee.DepName = department.DepName;
                _dbcontext.Set<Department>().Update(existingemployee);
                _dbcontext.SaveChanges();
                return Ok();
            }
            else return NotFound();
        }//Done

        [HttpDelete]
        [Route("Delete")]
        public ActionResult Delete(int id)
        {
            var record = _dbcontext.Set<Department>().Find(id);
            var employees = _dbcontext.Set<Employees>().ToList();
            if (record == null) return NotFound();
            
            foreach (var employee in employees)
            {
                if (employee.FDep_Id == id) { employee.IsDeleted = true; }
            }
     
           _dbcontext.Set<Department>().Remove(record);
           _dbcontext.SaveChanges();
           return Ok();
          
        }//Done
    }
}
