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
    [ApiController]
    [Route("[controller]")]
    public class WorksOnController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public WorksOnController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        [HttpGet(Name = "GetWorksOn")]
        public IActionResult GetAllWorksOn(int pageNum, int pageSize)
        {
            var dataWithMetadata = PageList<WorksOn>.CreateInstance(_unitOfWork.WorksOn.GetAll(), pageNum, pageSize);
            var WorksDto = new PageDto<WorksOn>
            {
                Data = dataWithMetadata,
                CurrentPage = dataWithMetadata.CurrentPage,
                TotalCount = dataWithMetadata.TotalCount,
                TotalPages = dataWithMetadata.TotalPages,
                NextLink = dataWithMetadata.HasNext ? CreateUrl("GetWorksOn", pageNum + 1, pageSize) : null,
                PreviousLink = dataWithMetadata.HasPrevious ? CreateUrl("GetWorksOn", pageNum - 1, pageSize) : null
            };
            return Ok(WorksDto);
        }

        private string CreateUrl(string endPoint, int pageNum, int pageSize)
        {
            return Url.Link(endPoint, new
            {
                pageNum,
                pageSize
            });
        }

        [HttpPost]
        public IActionResult AddNewWorksOn(WorksOn WorksOn)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var addedWorksOn = _unitOfWork.WorksOn.Add(WorksOn);
            if (addedWorksOn is null)
                return BadRequest();
            return CreatedAtRoute("GetWorksOn", new { pageNum = 1,pageSize=10 });

        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateWorksOn(int id, WorksOn WorksOn)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var updatedWorksOn = _unitOfWork.WorksOn.Update(WorksOn);
            if (updatedWorksOn is null)
                return NotFound();
            return Ok(updatedWorksOn);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteWorksOn(int id)
        {
            var deletedWorksOn = _unitOfWork.WorksOn.Delete(id);
            if (deletedWorksOn is null)
                return NotFound();
            return Ok(deletedWorksOn);
        }
    }
}
