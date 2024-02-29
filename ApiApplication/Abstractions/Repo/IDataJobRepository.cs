using System.Threading.Tasks;
using System.Threading;
using ApiApplication.Database.Entities;
using System.Collections;
using System.Collections.Generic;
using ApiApplication.Common;
using ApiApplication.Dto;
using System;

namespace ApiApplication.Abstractions.Repo
{
    public interface IDataJobRepository
    {
        IEnumerable<DataJobEntity> GetAll();
        IEnumerable<DataJobEntity> GetByStatus(int status);
        DataJobEntity GetById(Guid dataJobId);
        DataJobEntity Create(DataJobEntity dataJob);
        DataJobEntity Update(DataJobEntity dataJob);
        void Delete(Guid dataJob);
        bool StartBackgroundProcess(Guid dataJobId);
        int GetBackgroundProcessStatus(Guid dataJobId);
        IEnumerable<string> GetBackgroundProcessResults(Guid dataJobId);
        bool ExistsDataJob(Guid id);
    }
}
