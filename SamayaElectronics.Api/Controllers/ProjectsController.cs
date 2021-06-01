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
    public class ProjectsController:ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProjectsController(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        [HttpGet(Name = "GetProjects")]
        public IActionResult GetAllProjects(int pageNum, int pageSize)
        {
            var dataWithMetadata = PageList<Project>.CreateInstance(_unitOfWork.Project.GetAll(), pageNum, pageSize);
            var ProjectDto = new PageDto<Project>
            {
                Data = dataWithMetadata,
                CurrentPage = dataWithMetadata.CurrentPage,
                TotalCount = dataWithMetadata.TotalCount,
                TotalPages = dataWithMetadata.TotalPages,
                NextLink = dataWithMetadata.HasNext ? CreateUrl("GetProjects", pageNum + 1, pageSize) : null,
                PreviousLink = dataWithMetadata.HasPrevious ? CreateUrl("GetProjects", pageNum - 1, pageSize) : null
            };
            return Ok(ProjectDto);
        }

        private string CreateUrl(string endPoint, int pageNum, int pageSize)
        {
            return Url.Link(endPoint, new
            {
                pageNum,
                pageSize
            });
        }

        [HttpGet("{id}", Name = "GetProjectById")]
        public IActionResult GetProjectById(int id)
        {
            if (id == 0)
                return NotFound();
            var Project = _unitOfWork.Project.GetById(id);

            if (Project is null)
                return NotFound();

            return Ok(Project);
        }

        [HttpPost]
        public IActionResult AddNewProject(Project Project)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var addedProject = _unitOfWork.Project.Add(Project);
            if (addedProject is null)
                return BadRequest();
            return CreatedAtRoute("GetProjectById", new { id = Project.Id }, Project);

        }
        [HttpPut("{id:int}")]
        public IActionResult UpdateProject(int id, Project Project)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != Project.Id)
                return BadRequest("Two Id's Is not The Same");
            var updatedProject = _unitOfWork.Project.Update(Project);
            if (updatedProject is null)
                return NotFound();
            return Ok(updatedProject);
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteProject(int id)
        {
            var deletedProject = _unitOfWork.Project.Delete(id);
            if (deletedProject is null)
                return NotFound();
            return Ok(deletedProject);
        }
    }
}
