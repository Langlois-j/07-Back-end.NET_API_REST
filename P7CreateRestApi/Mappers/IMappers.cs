namespace Dot.Net.WebApi.Mappers
{
    public interface IMapper<TEntity, TDto>
    {
        TDto ToDTO(TEntity entity);
        TEntity ToEntity(TDto dto);
    }
}