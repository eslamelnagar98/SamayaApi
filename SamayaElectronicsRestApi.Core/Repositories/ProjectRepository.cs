using Microsoft.EntityFrameworkCore;
using SamayaElectronicsRestApi.Core.Interfaces;
using SamayaElectronicsRestApi.Domain.Entities;
using SamayaElectronicsRestApi.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SamayaElectronicsRestApi.Core.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly SamayaDbContext _samayaDbContext;
        public ProjectRepository(SamayaDbContext samayaDbContext)
        {
            this._samayaDbContext = samayaDbContext;
        }
        public Project Add(Project entity)
        {
            if (entity is null)
                return null;
            try
            {
                _samayaDbContext.Projects.Add(entity);
                _samayaDbContext.SaveChanges();
            }
            catch (Exception)
            {

                return null;
            }
            return entity;
        }

        public Project Delete(int id)
        {
            var Project = _samayaDbContext.Projects.Find(id);
            if (Project is null)
                return null;
            try
            {
                _samayaDbContext.Projects.Remove(Project);
                _samayaDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }
            return Project;

        }

        public IReadOnlyList<Project> GetAll()
            => _samayaDbContext.Projects.ToList();

        public Project GetById(int id)
        {
            var Project = _samayaDbContext.Projects.Find(id);
            if (Project is null)
                return null;
            return Project;
        }
        public Project Update(Project entity)
        {
            if (entity is null)
                return null;
            try
            {
                _samayaDbContext.Entry(entity).State = EntityState.Modified;
                _samayaDbContext.SaveChanges();
            }
            catch (Exception)
            {

                return null;
            }
            return entity;
        }
    }
}
