using ApiApplication.Abstractions.Repo;
using ApiApplication.Abstractions.Services;
using ApiApplication.Application.DataJobs.Commands;
using ApiApplication.Contracts;
using ApiApplication.Database.Entities;
using ApiApplication.Infrastructure.Common.Errors;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Application.DataJobs.Commands
{
    public class DeleteDataJobCommandHandler : IRequestHandler<DeleteDataJobCommand, OneOf<DeleteDataJobResponse, IList<Error>>>
    {
        private IDataJobRepository _dataJobRepository;
        private IDataProcessorService _dataProcessorService;

        public DeleteDataJobCommandHandler(IDataJobRepository dataJobRepository, IDataProcessorService dataProcessorService)
        {
            _dataJobRepository = dataJobRepository;
            _dataProcessorService = dataProcessorService;
        }

        public async Task<OneOf<DeleteDataJobResponse, IList<Error>>> Handle(DeleteDataJobCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();
            
            DataJobEntity dataJobToDelete;
            
            try
            {
                dataJobToDelete = _dataJobRepository.GetById(request.Id);

                if (dataJobToDelete == null)
                {
                   throw new DataException("DataJob not found");
                }

                _dataProcessorService.Delete(dataJobToDelete.Id);

                return new DeleteDataJobResponse();
            }
            catch (DataException ex)
            {
                errors.Add(Error.NotFound(DefaultErrorCodes.NotFound, "DataJob does not exist"));
                return errors;
            }
            catch (Exception ex)
            {
                errors.Add(Error.Internal());
                return errors;
            }
        }
    }
}
