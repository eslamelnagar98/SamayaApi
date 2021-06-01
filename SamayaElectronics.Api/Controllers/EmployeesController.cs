using Microsoft.AspNetCore.Mvc;
using SamayaElectronics.Api.DTO;
using SamayaElectronics.Api.Helpers;
using SamayaElectronicsRestApi.Core.Interfaces;
using SamayaElectronicsRestApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronics.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetEMps")]
        public IActionResult GetAllEmployee(int pageNum,int pageSize)
        {
            var dataWithMetadata = PageList<Employee>.CreateInstance(_unitOfWork.Employee.GetAll(), pageNum, pageSize);
            var empsDto = new PageDto<Employee>
            {
                Data = dataWithMetadata,
                CurrentPage = dataWithMetadata.CurrentPage,
                TotalCount = dataWithMetadata.TotalCount,
                TotalPages = dataWithMetadata.TotalPages,
                NextLink = dataWithMetadata.HasNext ? CreateUrl("GetEMps", pageNum + 1, pageSize) : null,
                PreviousLink = dataWithMetadata.HasPrevious ? CreateUrl("GetEMps", pageNum - 1, pageSize) : null
            };
            return Ok(empsDto);
        }

        private string CreateUrl(string endPoint, int pageNum, int pageSize)
        {
            return Url.Link(endPoint, new
            {
                pageNum,
                pageSize
            });
        }

        [HttpGet("{id}", Name = "GetEmployeeById")]
        public IActionResult GetEmployeeById(int id)
        {
            if (id == 0)
                return NotFound();
            var employee = _unitOfWork.Employee.GetById(id);

            if (employee is null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPost]
        public IActionResult AddNewEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var addedEmployee = _unitOfWork.Employee.Add(employee);
            if (addedEmployee is null)
                return BadRequest();
            return CreatedAtRoute("GetEmployeeById", new { id = employee.Id }, employee);

        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateEmployee(int id, Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != employee.Id)
                return BadRequest("Two Id's Is not The Same");
            var updatedEmployee = _unitOfWork.Employee.Update(employee);
            if (updatedEmployee is null)
                return NotFound();
            return Ok(updatedEmployee);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteEmployee(int id)
        {
            var deletedEmployee = _unitOfWork.Employee.Delete(id);
            if (deletedEmployee is null)
                return NotFound();
            return Ok(deletedEmployee);
        }
    }
}
