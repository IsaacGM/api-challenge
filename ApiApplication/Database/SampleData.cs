using ApiApplication.Common;
using ApiApplication.Database.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace ApiApplication.Database
{
    public class SampleData
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetService<DataJobsContext>();
            
            context.Database.EnsureCreated();

            var newStatus = new DataJobStatusEntity()
            {
                Id = (int)DataJobStatus.New,
                Name = DataJobStatus.New.ToString()
            };

            var processingStatus = new DataJobStatusEntity()
            {
                Id = (int)DataJobStatus.Processing,
                Name = DataJobStatus.Processing.ToString()
            };

            var completedStatus = new DataJobStatusEntity()
            {
                Id = (int)DataJobStatus.Completed,
                Name = DataJobStatus.Completed.ToString()
            };

            var listOfStatuses = new List<DataJobStatusEntity>
            {
                newStatus,
                processingStatus,
                completedStatus
            };

            context.DataJobStatuses.AddRange(listOfStatuses);

            for (int i = 0; i < 3 * listOfStatuses.Count; i++)
            {
                var dataJob = new DataJobEntity()
                {
                    Id = Guid.NewGuid(),
                    Name = "DataJob " + i,
                    FilePathToProcess = "C:\\Temp\\DataJob" + i + ".csv",
                    Status = listOfStatuses[i % listOfStatuses.Count]
                };

                context.DataJobs.Add(dataJob);
            }

            context.SaveChanges();
        }
    }
}
