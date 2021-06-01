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
    public class WorksOnRepository : IWorksOnRepository
    {
        private readonly SamayaDbContext _samayaDbContext;
        public WorksOnRepository(SamayaDbContext samayaDbContext)
        {
            this._samayaDbContext = samayaDbContext;
        }
        public WorksOn Add(WorksOn entity)
        {
            if (entity is null)
                return null;
            try
            {
                _samayaDbContext.WorksOns.Add(entity);
                _samayaDbContext.SaveChanges();
            }
            catch (Exception)
            {

                return null;
            }
            return entity;
        }

        public WorksOn Delete(int id)
        {
            var WorksOn = _samayaDbContext.WorksOns.Find(id);
            if (WorksOn is null)
                return null;
            try
            {
                _samayaDbContext.WorksOns.Remove(WorksOn);
                _samayaDbContext.SaveChanges();
            }
            catch (Exception)
            {
                return null;
            }
            return WorksOn;

        }

        public IReadOnlyList<WorksOn> GetAll()
            => _samayaDbContext.WorksOns.ToList();

        public WorksOn GetById(int id)
        {
            var WorksOn = _samayaDbContext.WorksOns.Find(id);
            if (WorksOn is null)
                return null;
            return WorksOn;
        }
        public WorksOn Update(WorksOn entity)
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
