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
    public class DepartmentsController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public DepartmentsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        [HttpGet(Name = "GetDepartments")]
        public IActionResult GetAllDepartments(int pageNum, int pageSize)
        {
            var dataWithMetadata = PageList<Department>.CreateInstance(_unitOfWork.Department.GetAll(), pageNum, pageSize);
            var DeptDto = new PageDto<Department>
            {
                Data = dataWithMetadata,
                CurrentPage = dataWithMetadata.CurrentPage,
                TotalCount = dataWithMetadata.TotalCount,
                TotalPages = dataWithMetadata.TotalPages,
                NextLink = dataWithMetadata.HasNext ? CreateUrl("GetDepartments", pageNum + 1, pageSize) : null,
                PreviousLink = dataWithMetadata.HasPrevious ? CreateUrl("GetDepartments", pageNum - 1, pageSize) : null
            };
            return Ok(DeptDto);
        }

        private string CreateUrl(string endPoint, int pageNum, int pageSize)
        {
            return Url.Link(endPoint, new
            {
                pageNum,
                pageSize
            });
        }

        [HttpGet("{id}", Name = "GetDepartmentById")]
        public IActionResult GetDepartmentById(int id)
        {
            if (id == 0)
                return NotFound();
            var Department = _unitOfWork.Department.GetById(id);

            if (Department is null)
                return NotFound();

            return Ok(Department);
        }

        [HttpPost]
        public IActionResult AddNewDepartment(Department Department)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var addedDepartment = _unitOfWork.Department.Add(Department);
            if (addedDepartment is null)
                return BadRequest();
            return CreatedAtRoute("GetDepartmentById", new { id = Department.Id }, Department);

        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateDepartment(int id, Department Department)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != Department.Id)
                return BadRequest("Two Id's Is not The Same");
            var updatedDepartment = _unitOfWork.Department.Update(Department);
            if (updatedDepartment is null)
                return NotFound();
            return Ok(updatedDepartment);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteDepartment(int id)
        {
            var deletedDepartment = _unitOfWork.Department.Delete(id);
            if (deletedDepartment is null)
                return NotFound();
            return Ok(deletedDepartment);
        }
    }
}
