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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SamayaDbContext _samayaDbContext;
        public EmployeeRepository(SamayaDbContext samayaDbContext)
        {
            this._samayaDbContext = samayaDbContext;
        }
        public Employee Add(Employee entity)
        {
            if (entity is null)
                return null;
            try
            {
                _samayaDbContext.Employees.Add(entity);
                _samayaDbContext.SaveChanges();
            }
            catch (Exception)
            {

                return null;
            }
            return entity;
        }

        public Employee Delete(int id)
        {
            var employee = _samayaDbContext.Employees.Find(id);
            if (employee is null)
                return null;
            try
            {
                _samayaDbContext.Employees.Remove(employee);
                _samayaDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }
            return employee;

        }

        public IReadOnlyList<Employee> GetAll()
            => _samayaDbContext.Employees.ToList();

        public Employee GetById(int id)
        {
            var employee = _samayaDbContext.Employees.Find(id);
            if (employee is null)
                return null;
            return employee;
        }


        public Employee Update(Employee entity)
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
