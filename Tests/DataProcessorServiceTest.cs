namespace Tests
{
    using NUnit.Framework;
    using NSubstitute;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using ApiApplication.Abstractions.Services;
    using ApiApplication.Application.Services;
    using ApiApplication.Common;
    using ApiApplication.Dto;
    using ApiApplication.Abstractions.Repo;
    using ApiApplication.Abstractions.Processors;
    using ApiApplication.Database.Entities;
    using NSubstitute.ExceptionExtensions;
    using System.Data;

    [TestFixture]
    public class DataProcessorServiceTests
    {
        private IDataProcessorService _dataProcessorService;
        private IDataJobRepository _dataJobRepositoryMock;
        private IProcessStrategyFactory _processStrategyFactoryMock;
        private ILinkGeneratorService _linkGeneratorServiceMock;

        [SetUp]
        public void SetUp()
        {
            _dataJobRepositoryMock = Substitute.For<IDataJobRepository>();
            _processStrategyFactoryMock = Substitute.For<IProcessStrategyFactory>();
            _linkGeneratorServiceMock = Substitute.For<ILinkGeneratorService>();
            _dataProcessorService = new DataProcessorService(_dataJobRepositoryMock, _processStrategyFactoryMock, _linkGeneratorServiceMock);
            MapsterLoader.AddMapsterFor(typeof(DataProcessorService));
        }

        [Test]
        public void GetAllDataJobs_ReturnsDataJobs()
        {
            // Arrange
            
            var mockedDatabase = new TestHelper();
            List<DataJobDTO> expectedDataJobs = mockedDatabase.DataJobs;
            List<DataJobEntity> dataJobEntities = expectedDataJobs.Select(mockedDatabase.ConvertDataJobToEntity).ToList<DataJobEntity>();

            _dataJobRepositoryMock.GetAll().Returns(dataJobEntities);

            // Act
            List<DataJobDTO> result = new List<DataJobDTO>(_dataProcessorService.GetAllDataJobs());

            // Assert
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(result[i].Id, Is.EqualTo(expectedDataJobs[i].Id));
                Assert.That(result[i].Name, Is.EqualTo(expectedDataJobs[i].Name));
                Assert.That(result[i].FilePathToProcess, Is.EqualTo(expectedDataJobs[i].FilePathToProcess));
                Assert.That(result[i].Status, Is.EqualTo(expectedDataJobs[i].Status));
            }
        }

        [Test]
        public void GetDataJobsByStatus_ReturnsFilteredDataJobs()
        {
            // Arrange
            var statusToFilter = DataJobStatus.New;
            var mockedDatabase = new TestHelper();

            List<DataJobDTO> expectedDataJobs = mockedDatabase.DataJobs.Where(d => d.Status == statusToFilter).ToList();
            List<DataJobEntity> queriedDataJobs = expectedDataJobs.Select(mockedDatabase.ConvertDataJobToEntity).ToList();

            _dataJobRepositoryMock.GetByStatus(Arg.Any<int>()).Returns(queriedDataJobs);

            // Act
            var result = _dataProcessorService.GetDataJobsByStatus(statusToFilter).ToList();

            // Assert
            for (int i = 0; i < result.Count; i++)
            {
                Assert.That(result[i].Id, Is.EqualTo(expectedDataJobs[i].Id));
                Assert.That(result[i].Name, Is.EqualTo(expectedDataJobs[i].Name));
                Assert.That(result[i].FilePathToProcess, Is.EqualTo(expectedDataJobs[i].FilePathToProcess));
                Assert.That(result[i].Status, Is.EqualTo(expectedDataJobs[i].Status));
            }
        }


        [Test]
        public void Delete_ValidDataJobId_ReturnsVoid()
        {
            // Arrange
            var validDataJobId = Guid.NewGuid();

            _dataJobRepositoryMock.Delete(Arg.Any<Guid>());

            // Act
            _dataProcessorService.Delete(validDataJobId);

            // Assert
            _dataJobRepositoryMock.Received(1).Delete(Arg.Is(validDataJobId));
        }

        [Test]
        public void Create_ValidDataJob_ReturnsCreatedDataJob()
        {
            // Arrange
            var newJobId = Guid.NewGuid();
            var dataJobToCreate = new DataJobDTO(newJobId, "Job3", "path3.csv", DataJobStatus.New, new List<string>());
            var dataJobEntity = new DataJobEntity(newJobId, "Job3", "path3.csv", (int)DataJobStatus.New, new List<ResultEntity>());

            // Assuming _dataJobRepositoryMock is a substitute for IDataJobRepository

            _dataJobRepositoryMock.Create(Arg.Any<DataJobEntity>()).Returns(x => x.ArgAt<DataJobEntity>(0));

            //Act
            var result = _dataProcessorService.Create(dataJobToCreate);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(dataJobToCreate.Id));
        }

        [Test]
        public void Update_ValidDataJob_ReturnsUpdatedDataJob()
        {
            // Arrange
            var helper = new TestHelper();
            var baseDataJob = helper.GetDataJob(0);
            DataJobEntity returnedDataJobEntity = new DataJobEntity(baseDataJob.Id, "Job3-3", "path3-3.csv", (int)DataJobStatus.New, new List<ResultEntity>());
            DataJobDTO dataJobToUpdate = new DataJobDTO(baseDataJob.Id, "Job3-3", "path3-3.csv", DataJobStatus.New, new List<string>());

            _dataJobRepositoryMock.GetById(Arg.Any<Guid>())
                .Returns(info => returnedDataJobEntity);

            _dataJobRepositoryMock.ExistsDataJob(Arg.Any<Guid>())
                .Returns(dj => true);

            _dataJobRepositoryMock.Update(Arg.Any<DataJobEntity>())
                .Returns(info => returnedDataJobEntity);

            // Act
            var result = _dataProcessorService.Update(dataJobToUpdate);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(returnedDataJobEntity.Id == result.Id);
            Assert.That(returnedDataJobEntity.Name.Equals(result.Name));
            Assert.That(returnedDataJobEntity.FilePathToProcess.Equals(result.FilePathToProcess));
        }


        [Test]
        public void Update_NotValidDataJob_ThrowsException()
        {
            // Arrange
            var helper = new TestHelper();
            var baseDataJobId = Guid.NewGuid();
            DataJobEntity returnedDataJobEntity = new DataJobEntity(baseDataJobId, "Job3-3", "path3-3.csv", (int)DataJobStatus.New, new List<ResultEntity>());
            DataJobDTO dataJobToUpdate = new DataJobDTO(baseDataJobId, "Job3-3", "path3-3.csv", DataJobStatus.New, new List<string>());

            _dataJobRepositoryMock.Update(Arg.Any<DataJobEntity>()).Returns(r => r.Arg<DataJobEntity>());

            // Act
            TestDelegate act = () => _dataProcessorService.Update(dataJobToUpdate);

            // Assert
            Assert.Throws<DataException>(act);
        }

        private class TestHelper
        {
            private List<Guid> _guids = new List<Guid>()
            {
                new Guid("71d8d94c-073c-4f27-ac17-11099a60cf87"),
                new Guid("6e9bb30f-be7e-433a-96b9-249b08c95adf"),
                new Guid("823a9c0c-f05a-41a8-b98e-ea33da0b7504"),
                new Guid("df3a92e9-455f-4236-8d31-4b80354b82ea"),
                new Guid("09d634b9-8190-4680-b075-dc09b0efa149"),
                new Guid("0d04b216-8d9b-4ec8-8aae-571e9642ecba"),
                new Guid("29074b7c-7119-4bcb-9ec4-c519cf00809e"),
                new Guid("c803e6ee-f44d-47f7-a434-65fecf143967"),
                new Guid("bf4a8df2-0b77-4551-99cb-b5d9b2eb08c5"),
            };

            private string _nameBase = "Datajob";
            private string _filePathBase = "C:\\Temp\\DataJob";
            private string _resultsBase = "C:\\Temp\\DataJob_{0}_{1}_result.csv";
            private int[] _statuses = { 1, 2, 3 };
            private List<string> _extensions = new List<string> { ".csv", ".txt", ".bin" };

            public List<DataJobDTO> DataJobs = new List<DataJobDTO>();

            public TestHelper()
            {
                for (int i = 0; i < _guids.Count; i++)
                {
                    DataJobDTO dataJob = CreateDatajob(i);

                    DataJobs.Add(dataJob);
                }
            }

            private DataJobDTO CreateDatajob(int i)
            {
                return new DataJobDTO
                {
                    Id = _guids[i],
                    Name = $"{_nameBase} {i}",
                    FilePathToProcess = $"{_filePathBase}{i}{_extensions[i % 3]}",
                    Status = (DataJobStatus) (i %_statuses.Length) ,
                    Results = GetResults(i)
                };
            }

            private IEnumerable<string> GetResults(int jobIndex)
            {
                for (int resultIndex = 0; resultIndex < 3; resultIndex++)
                {
                      yield return string.Format(_resultsBase, jobIndex, resultIndex);
                }
            }

            public DataJobDTO GetDataJob(int index)
            {
                return DataJobs[index];
            }

            public DataJobEntity GetDataJobAsEntity(int index)
            {
                return ConvertDataJobToEntity(DataJobs[index]);
            }

            public DataJobEntity ConvertDataJobToEntity(DataJobDTO dataJob)
            {
                var results = dataJob.Results?.Select(result => new ResultEntity(dataJob.Id, result)).ToList();
                DataJobEntity mappedEntity = new DataJobEntity(
                    dataJob.Id,
                    dataJob.Name,
                    dataJob.FilePathToProcess,
                    (int)dataJob.Status,
                    results);

                return mappedEntity;
            }

            public DataJobEntity CloneEntity(DataJobEntity dataJob)
            {
                DataJobEntity clonedEntity = new DataJobEntity(
                    dataJob.Id,
                    dataJob.Name,
                    dataJob.FilePathToProcess,
                    dataJob.StatusId,
                    dataJob.Results);

                return clonedEntity;
            }
        }
    }
}
