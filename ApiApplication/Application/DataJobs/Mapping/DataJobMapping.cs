using ApiApplication.Application.DataJobs.Commands;
using ApiApplication.Common;
using ApiApplication.Contracts;
using ApiApplication.Database.Entities;
using ApiApplication.Dto;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Application.DataJobs.Mapping
{
    public class DataJobMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<DataJobDTO, DataJobEntity>()
                .MapWith(source => new DataJobEntity
                {
                    Id = source.Id,
                    Name = source.Name,
                    FilePathToProcess = source.FilePathToProcess,
                    StatusId = (int) source.Status 
                });

            config.NewConfig<DataJobEntity, DataJobDTO>()
                .MapWith(source => new DataJobDTO(
                    source.Id,
                    source.Name,
                    source.FilePathToProcess,
                    (DataJobStatus)source.StatusId,
                    source.Results.Select(r => r.Path).ToList()
                ));

            config.NewConfig<CreateDataJobRequest, CreateDataJobCommand>()
                .MapWith(source => new CreateDataJobCommand(
                    new DataJobDTO(source.Id,
                                    source.Name,
                                    source.FilePathToProcess,
                                    source.Status,
                                    new List<string>()
                )));

            config.NewConfig<UpdateDataJobRequest, UpdateDataJobCommand>()
                .MapWith(source => new UpdateDataJobCommand(
                    new DataJobDTO(source.Id,
                                    source.Name,
                                    source.FilePathToProcess,
                                    (DataJobStatus)source.Status,
                                    new List<string>()
            )));

            config.NewConfig<UpdateDataJobRequest, UpdateDataJobCommand>()
                .MapWith(source => new UpdateDataJobCommand(
                    new DataJobDTO(source.Id,
                                    source.Name,
                                    source.FilePathToProcess,
                                    (DataJobStatus)source.Status,
                                    new List<string>()
            )));

            config.NewConfig<StartBackgroundProcessRequest, StartBackgroundProcessCommand>()
                .MapWith(source => new StartBackgroundProcessCommand(source.Id));

            config.NewConfig<DataJobEntity, DataJobDTO>()
                .MapWith((source) => new DataJobDTO(
                        source.Id,
                        source.Name,
                        source.FilePathToProcess,
                        (DataJobStatus)source.StatusId,
                        source.Results.Select(r => r.Path).ToList()))
                .MaxDepth(1)
                .PreserveReference(true);
        }
    }
}
