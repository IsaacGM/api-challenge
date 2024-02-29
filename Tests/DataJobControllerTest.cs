using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApiApplication.Application.DataJobs.Commands;
using ApiApplication.Application.DataJobs.Queries;
using ApiApplication.Controllers;
using ApiApplication.Dto;
using ApiApplication.Infrastructure.Common.Errors;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using OneOf;
using NSubstitute;
using ApiApplication.Contracts;
using Microsoft.AspNetCore.Http;

[TestFixture]
public class DataJobControllerTests
{
    private ISender _mediator;
    private IMapper _mapper;

    [SetUp]
    public void Setup()
    {
        _mediator = Substitute.For<ISender>();
        _mapper = Substitute.For<IMapper>();
    }

    [Test]
    public async Task GetDataJob_ReturnsDataJob()
    {
        // Arrange
        var controller = new DataJobController(_mediator, _mapper);
        var dataJobId = Guid.NewGuid();
        var dataJobDto = new DataJobDTO { Id = dataJobId, Name = "TestJob" };

        _mediator.Send(Arg.Any<GetDataJobByIdQuery>())
            .Returns(new GetDataJobByIdResponse(dataJobDto));

        // Act
        var result = await controller.GetDataJob(dataJobId);

        // Assert
        var okResult = result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null);
        Assert.That(200 == okResult.StatusCode, Is.True);
        Assert.That(dataJobDto.Id == ((GetDataJobByIdResponse)okResult.Value).DataJob.Id, Is.True);
    }


    [Test]
    public async Task GetDataJob_WhenDataJobNotFound_ReturnsNotFoundResult()
    {
        // Arrange
        var controller = new DataJobController(_mediator, _mapper);
        var notFoundDataJobId = Guid.NewGuid();
        var errors = new List<Error> { Error.NotFound(DefaultErrorCodes.NotFound, "Not found") };
        var notFoundQueryResponse = OneOf<GetDataJobByIdResponse, IList<Error>>.FromT1(
            errors
        );
        var httpContext = new DefaultHttpContext();
        
        controller.ControllerContext = new ControllerContext
        {
            HttpContext = httpContext
        };

        _mediator.Send(Arg.Any<GetDataJobByIdQuery>()).Returns(notFoundQueryResponse);

        // Act
        var result = await controller.GetDataJob(notFoundDataJobId);

        // Assert
        var notFoundResult = result as ObjectResult;
        Assert.That(notFoundResult, Is.Not.Null);
        Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
    }

    [TearDown]
    public void TearDown()
    {
        _mediator.ClearReceivedCalls();
    }

}
