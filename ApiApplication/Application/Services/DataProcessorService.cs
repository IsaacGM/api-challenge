using ApiApplication.Contracts;
using ApiApplication.Abstractions.Services;
using ApiApplication.Common;
using ApiApplication.Database.Entities;
using ApiApplication.Dto;
using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using ApiApplication.Abstractions.Repo;
using ApiApplication.Abstractions.Processors;
using System.Linq;
using Microsoft.IdentityModel.Protocols;
using System.Data;

namespace ApiApplication.Application.Services
{
    public class DataProcessorService : IDataProcessorService
    {
        private IDataJobRepository _dataJobRepository;
        private IProcessStrategyFactory _processStrategyFactory;
        private ILinkGeneratorService _linkGenerator;

        public DataProcessorService(IDataJobRepository dataJobRepository, 
            IProcessStrategyFactory processStrategyFactory,
            ILinkGeneratorService linkGenerator)
        {
            _dataJobRepository = dataJobRepository;
            _processStrategyFactory = processStrategyFactory;
            _linkGenerator = linkGenerator;
        }

        public DataJobDTO Create(DataJobDTO dataJob)
        {
            var newDataJob = new DataJobEntity
            {
                Id = dataJob.Id,
                Name = dataJob.Name,
                FilePathToProcess = dataJob.FilePathToProcess,
                StatusId = (int) dataJob.Status,
                Results = new List<ResultEntity>()
            };

            var resultDataJob = _dataJobRepository.Create(newDataJob);
            var resultDataJobDTO = resultDataJob.Adapt<DataJobDTO>();

            resultDataJobDTO.Links = _linkGenerator.GenerateForDatajobDTO(resultDataJobDTO);

            return resultDataJobDTO;
        }

        public void Delete(Guid dataJobID)
        {
            _dataJobRepository.Delete(dataJobID);
        }

        public IEnumerable<DataJobDTO> GetAllDataJobs()
        {
            var dataJobs = _dataJobRepository.GetAll();
            var resultDatajobs = dataJobs.Adapt<List<DataJobDTO>>();

            foreach (var dataJob in resultDatajobs)
            {
                dataJob.Links = _linkGenerator.GenerateForDatajobDTO(dataJob);
            }

            return resultDatajobs;
        }

        public List<string> GetBackgroundProcessResults(Guid dataJobId)
        {
            var dataJob = _dataJobRepository.GetById(dataJobId);

            if (dataJob is null)
            {
                throw new DataException("Datajob does not exists.");
            }

            if (dataJob.Status.Id != (int)DataJobStatus.Completed)
            {
                throw new InvalidOperationException("The process is not completed yet.");
            }

            return dataJob.Results.Select(r => r.Path).ToList();
        }

        public DataJobStatus GetBackgroundProcessStatus(Guid dataJobId)
        {
            var dataJob = _dataJobRepository.GetById(dataJobId);

            return (DataJobStatus)dataJob.StatusId;
        }

        public DataJobDTO GetDataJob(Guid id)
        {
            var dataJob = _dataJobRepository.GetById(id);
            var dataJobDTO = dataJob.Adapt<DataJobDTO>();

            dataJobDTO.Links = _linkGenerator.GenerateForDatajobDTO(dataJobDTO);

            return dataJobDTO;
        }

        public IEnumerable<DataJobDTO> GetDataJobsByStatus(DataJobStatus status)
        {
            var dataJobs = _dataJobRepository.GetByStatus((int)status);
            var resultDatajobs = dataJobs.Adapt<List<DataJobDTO>>();

            foreach (var dataJob in resultDatajobs)
            {
                dataJob.Links = _linkGenerator.GenerateForDatajobDTO(dataJob);
            }

            return resultDatajobs;
        }

        public bool StartBackgroundProcess(Guid dataJobId)
        {
            var dataJob = _dataJobRepository.GetById(dataJobId);
            var dataJobExtension =  System.IO.Path.GetExtension(dataJob.FilePathToProcess);
            var processStrategy = _processStrategyFactory.Create(dataJobExtension);           
            
            try
            { 
                //This should be a background process and just return true if the process was started
                //But for the sake of the test, we will just call the process and save the results into the database
                var processResult = processStrategy.Process(dataJob.FilePathToProcess);

                dataJob.StatusId = (int)DataJobStatus.Completed;
                dataJob.Results = processResult.Select(result => new ResultEntity() { Path = result }).ToList();
                _dataJobRepository.Update(dataJob);

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public DataJobDTO Update(DataJobDTO dataJob)
        {

            if (!_dataJobRepository.ExistsDataJob(dataJob.Id))
            {
                throw new DataException("Job not found");
            }

            if (!Enum.IsDefined(typeof(DataJobStatus), dataJob.Status))
            {
                throw new InvalidOperationException("Invalid DataJobStatus");
            }
            
            var dataJobEntity = _dataJobRepository.GetById(dataJob.Id);

            dataJobEntity.Name = dataJob.Name;
            dataJobEntity.FilePathToProcess = dataJob.FilePathToProcess;

            var updatedDataJob = _dataJobRepository.Update(dataJobEntity);
            var returnDataJob = updatedDataJob.Adapt<DataJobDTO>();

            returnDataJob.Links = _linkGenerator.GenerateForDatajobDTO(returnDataJob);

            return returnDataJob;
        }        
    }
}
