using Dot.Net.WebApi.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace P7CreateRestApi.Tests.Helpers
{
    public class FakeMapper<TEntity, TDto> : IMapper<TEntity, TDto>
           where TEntity : class, new()
           where TDto : class, new()
    {
        public TDto ToDTO(TEntity entity)
        {
            var dto = new TDto();
            foreach (var prop in typeof(TDto).GetProperties())
            {
                var src = typeof(TEntity).GetProperty(prop.Name);
                if (src != null && prop.CanWrite)
                    prop.SetValue(dto, src.GetValue(entity));
            }
            return dto;
        }

        public TEntity ToEntity(TDto dto)
        {
            var entity = new TEntity();
            foreach (var prop in typeof(TEntity).GetProperties())
            {
                var src = typeof(TDto).GetProperty(prop.Name);
                if (src != null && prop.CanWrite)
                    prop.SetValue(entity, src.GetValue(dto));
            }
            return entity;
        }
    }
}
