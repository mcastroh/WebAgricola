using AutoMapper;

namespace Logistic.BackEnd.API.Mappings;

public static class AutomapperGeneric<TSource, TDestination>
{
    private static Mapper _myMapper = new Mapper(new MapperConfiguration(
        cfg => cfg.CreateMap<TSource, TDestination>().ReverseMap()));

    public static TDestination Map(TSource source)
    {
        return _myMapper.Map<TDestination>(source);
    }

    public static List<TDestination> MapList(List<TSource> source)
    {
        var list = new List<TDestination>();
        source.ForEach(x => { list.Add(Map(x)); });
        return list;
    }
}