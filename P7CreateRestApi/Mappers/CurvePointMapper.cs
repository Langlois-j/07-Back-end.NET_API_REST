using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;


namespace Dot.Net.WebApi.Mappers
{
    public class CurveMapper : IMapper<CurvePoint, CurveDTO>
    {
        public CurveDTO ToDTO(CurvePoint entity)
        {
            return new CurveDTO
            {
                  Id = entity.Id,
        CurveId = entity.CurveId,
                // AsOfDate = entity.AsOfDate,
                Term = entity.Term,
        CurvePointValue = entity.CurvePointValue,
            };
        }

        public CurvePoint ToEntity(CurveDTO dto)
        {
            return new CurvePoint
            {
                 Id = dto.Id ,
                CurveId = dto.CurveId,
                // AsOfDate = dto.AsOfDate,
                Term = dto.Term,
                CurvePointValue = dto.CurvePointValue,
            };
        }
    }
}