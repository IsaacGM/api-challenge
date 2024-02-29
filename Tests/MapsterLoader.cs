using Mapster;
using MapsterMapper;
using System.Reflection;

public static class MapsterLoader
{
    private static Mapper MAPPER;

    internal static void AddMapsterFor(Type type)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(type.Assembly);
        MAPPER = new Mapper(config);
    }

    public static Mapper GetMapper()
    {
        return MAPPER;
    }
}