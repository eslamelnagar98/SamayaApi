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
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly SamayaDbContext _samayaDbContext;
        public DepartmentRepository(SamayaDbContext samayaDbContext)
        {
            this._samayaDbContext = samayaDbContext;
        }
        public Department Add(Department entity)
        {
            if (entity is null)
                return null;
            try
            {
                _samayaDbContext.Departments.Add(entity);
                _samayaDbContext.SaveChanges();
            }
            catch (Exception)
            {

                return null;
            }
            return entity;
        }

        public Department Delete(int id)
        {
            var Department = _samayaDbContext.Departments.Find(id);
            if (Department is null)
                return null;
            try
            {
                _samayaDbContext.Departments.Remove(Department);
                _samayaDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }
            return Department;

        }

        public IReadOnlyList<Department> GetAll()
            => _samayaDbContext.Departments.ToList();

        public Department GetById(int id)
        {
            var Department = _samayaDbContext.Departments.Find(id);
            if (Department is null)
                return null;
            return Department;
        }


        public Department Update(Department entity)
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
