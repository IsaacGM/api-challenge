using ApiApplication.Application.DataJobs.Commands;
using ApiApplication.Contracts;
using ApiApplication.Database.Entities;
using Mapster;

namespace ApiApplication.Application.Result.Mapping
{
    public class ResultMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ResultEntity, string>()
                .MapWith(source => source.Path);

            config.NewConfig<string, ResultEntity>()
                .MapWith(source => new ResultEntity()
                {
                    Path = source
                });
        }
    }
}
