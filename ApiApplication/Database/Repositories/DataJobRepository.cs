using ApiApplication.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;
using System.Linq;
using ApiApplication.Common;
using System.Data;
using ApiApplication.Abstractions.Repo;
using Mapster;

namespace ApiApplication.Database.Repositories
{
    public class DataJobRepository : IDataJobRepository
    {
        private readonly DataJobsContext _context;

        public DataJobRepository(DataJobsContext context)
        {
            _context = context;
        }
        
        public IEnumerable<DataJobEntity> GetAll()
        {
            return _context.DataJobs
                .Include(dataJob => dataJob.Status)
                .Include(dataJob => dataJob.Results)
                .ToList();
        }

        public IEnumerable<DataJobEntity> GetByStatus(int status)
        {
            var statusEntity = _context.DataJobStatuses.FirstOrDefault(st => st.Id == status);

            if (statusEntity is null or { Id: 0 })
            {
                throw new DataException("Status not found");
            }

            return _context.DataJobs.Where(dataJob => dataJob.Status.Id == status).ToList();
        }

        public DataJobEntity GetById(Guid dataJobId)
        {
            if (! ExistsDataJob(dataJobId))
            {
                return null;
            }

            return _context.DataJobs
                .Include(dataJob => dataJob.Status)
                .Include(dataJob => dataJob.Results)
                .FirstOrDefault(dataJob => dataJob.Id == dataJobId);
        }

        public DataJobEntity Create(DataJobEntity dataJob)
        {
            if (ExistsDataJob(dataJob.Id))
            {
                throw new DataException("Data job already exists");
            }

            var createdDataJob = _context.DataJobs.Add(dataJob);
            _context.SaveChanges();
            return createdDataJob.Entity;
        }

        public DataJobEntity Update(DataJobEntity dataJob)
        {
            var updatedDataJob = _context.DataJobs.Update(dataJob);
            _context.SaveChanges();
            return updatedDataJob.Entity;
        }

        public void Delete(Guid dataJobId)
        {
            if (!ExistsDataJob(dataJobId))
            {
                throw new DataException("Data job not found");
            }

            var dataJobToDelete = _context.DataJobs.FirstOrDefault(dataJob => dataJob.Id == dataJobId);
            
            _context.DataJobs.Remove(dataJobToDelete);
            _context.SaveChanges();
        }

        public bool StartBackgroundProcess(Guid dataJobId)
        {

            if (!ExistsDataJob(dataJobId))
            {
                throw new DataException("Data job not found");
            }

            var dataJob = _context.DataJobs.FirstOrDefault(dataJob => dataJob.Id == dataJobId);

            if (dataJob.Status.Id != (int) DataJobStatus.Processing)
            {
                return false;
            }

            dataJob.Status.Id = (int) DataJobStatus.Processing;
            _context.SaveChanges();

            return true;
        }

        public int GetBackgroundProcessStatus(Guid dataJobId)
        {
            var dataJob = _context.DataJobs.FirstOrDefault(dataJob => dataJob.Id == dataJobId);
            
            return dataJob.Status.Id;
        }

        public IEnumerable<string> GetBackgroundProcessResults(Guid dataJobId)
        {
            var dataJob = _context.DataJobs.FirstOrDefault(dataJob => dataJob.Id == dataJobId);

            return dataJob.Results.Adapt<IEnumerable<string>>();

        }

        public bool ExistsDataJob(Guid id)
        {
            return _context.DataJobs.Any(dj => dj.Id == id);
        }
    }
}
