using ApiApplication.Abstractions.Services;
using ApiApplication.Application.DataJobs.Commands;
using ApiApplication.Application.DataJobs.Queries;
using ApiApplication.Contracts;
using ApiApplication.Infrastructure.Common.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DataJobController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;

        public DataJobController(ISender sender, IMapper mapper)
        {
            _mediator = sender;
            _mapper = mapper;
        }

        /// <summary>
        /// Retrieves all the data jobs in the database.
        /// </summary>
        /// <returns>An instance of GetAllDataJobsResponse containing the collection of DatajobDTO</returns>
        [HttpGet(Name = "GetDataJobs")]
        public async Task<IActionResult> GetDataJobs()
        {
            var getAllDataJobsQuery = new GetAllDataJobsQuery();
            OneOf<GetAllDataJobsResponse, IList<Error>> getAllDataJobsResponse = await _mediator.Send(getAllDataJobsQuery);

            var result = getAllDataJobsResponse.Match(
                listDataJobsResponse => Ok(listDataJobsResponse),
                errors => Problem(errors)
            );

            return result;
        }

        /// <summary>
        /// Gets the data jobs by status. 
        /// </summary>
        /// <param name="status">Status identifier</param>
        /// <returns>An instance of getDataJobsByStatusResponse containing the collection of DatajobDTO</returns>
        [HttpGet("bystatus/{status}", Name = "GestDataJobsByStatus")]
        public async Task<IActionResult> GetProcessesByStatus(int status)
        {
            var getDataJobsByStatusQuery = new GetDataJobsByStatusQuery(status);
            OneOf<GetDataJobsByStatusResponse, IList<Error>> getDataJobsByStatusResponse = await _mediator.Send(getDataJobsByStatusQuery);

            return getDataJobsByStatusResponse.Match(
                dataJob => Ok(dataJob),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Returns one datajob by its identifier.
        /// </summary>
        /// <param name="id">Guid identifing the datajob</param>
        /// <returns>Instance of GetDataJobByIdResponse</returns>
        [HttpGet("{id}", Name = "GetDataJobById")]
        public async Task<IActionResult> GetDataJob(Guid id)
        {
            var getDataJobByIdQuery = new GetDataJobByIdQuery(id);
            OneOf<GetDataJobByIdResponse, IList<Error>> getDataJobByIdResponse = await _mediator.Send(getDataJobByIdQuery);

            return getDataJobByIdResponse.Match(
                dataJob => Ok(dataJob),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Creates a new datajob
        /// </summary>
        /// <param name="request">Instance of CreateDataJobRequest containing the new datajob data</param>
        /// <returns>CreateDataJobResponse with the data of the new datajob</returns>
        [HttpPost(Name = "CreateDataJob")]
        public async Task<IActionResult> Create([FromBody] CreateDataJobRequest request)
        {
            var createDataJobCommand = _mapper.Map<CreateDataJobCommand>(request);
            OneOf<CreateDataJobResponse, IList<Error>> createDataJobResponse = await _mediator.Send(createDataJobCommand);

            return createDataJobResponse.Match(
                createdataJobResponse => Ok(createdataJobResponse),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Updates the datajob with the new data
        /// </summary>
        /// <param name="request">CreateDataJobRequest instance containing the new Data</param>
        /// <returns>Instance of UpdateDataJobResponse</returns>
        [HttpPut(Name = "UpdateDataJob")]
        public async Task<IActionResult> Update([FromBody] UpdateDataJobRequest request)
        {
            var updateDataJobCommand = _mapper.Map<UpdateDataJobCommand>(request);
            OneOf<UpdateDataJobResponse, IList<Error>> updateDataJobResponse = await _mediator.Send(updateDataJobCommand);

            return updateDataJobResponse.Match(
                updatedDataJobResponse => Ok(updatedDataJobResponse),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Removes a datajob from the database
        /// </summary>
        /// <param name="id">Guid identifying the datajob</param>
        /// <returns><void/returns>
        [HttpDelete("{id}", Name = "DeleteJob")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleteDataJobCommand = new DeleteDataJobCommand(id);
            OneOf<DeleteDataJobResponse, IList<Error>> deteleDataJobResponse = await _mediator.Send(deleteDataJobCommand);

            return deteleDataJobResponse.Match(
                deteleDataJobResponse =>  Ok(),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Starts a background process
        /// </summary>
        /// <param name="id">The datajob's guid</param>
        /// <returns>True if started.</returns>
        [HttpPost("StartProcess", Name = "StartProcess")]
        public async Task<IActionResult> StartBackgroundProcess(Guid id)
        {
            var startBackgroundProcessCommand = new StartBackgroundProcessCommand(id);
            OneOf<StartBackgroundProcessResponse, IList<Error>> startBackgroundProcessResponse = await _mediator.Send(startBackgroundProcessCommand);

            return startBackgroundProcessResponse.Match(
                startBackgroundProcessResponse => Ok(startBackgroundProcessResponse),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Gets the status of a background process
        /// </summary>
        /// <param name="id">Datajob identifier</param>
        /// <returns>Instance of GetBackgroundProcessStatusResponse</returns>
        [HttpGet("{id}/status", Name = "GetByStatus")]
        public async Task<IActionResult> GetBackgroundProcessStatus(Guid id)
        {
            var getBackgroundProcessStatusQuery = new GetBackgroundProcessStatusQuery(id);
            OneOf<GetBackgroundProcessStatusResponse, IList<Error>> getBackgroundProcessStatusResponse = await _mediator.Send(getBackgroundProcessStatusQuery);

            return getBackgroundProcessStatusResponse.Match(
                getBackgroundProcessStatusResponse => Ok(getBackgroundProcessStatusResponse),
                errors => Problem(errors)
            );
        }

        /// <summary>
        /// Gets a list of the files produced by the background process
        /// </summary>
        /// <param name="id">Datajob identifier</param>
        /// <returns>Instance of GetBackgroundProcessResultsResponse containing the file list</returns>
        [HttpGet("{id}/results", Name = "GetBackgroundProcessResults")]
        public async Task<IActionResult> GetBackgroundProcessResults(Guid id)
        {
            var getBackgroundProcessResultsQuery = new GetBackgroundProcessResultsQuery(id);
            OneOf<GetBackgroundProcessResultsResponse, IList<Error>> getBackgroundProcessResultsResponse = await _mediator.Send(getBackgroundProcessResultsQuery);

            return getBackgroundProcessResultsResponse.Match(
                getBackgroundProcessResultsResponse => Ok(getBackgroundProcessResultsResponse),
                errors => Problem(errors)
            );
        }
    }
}