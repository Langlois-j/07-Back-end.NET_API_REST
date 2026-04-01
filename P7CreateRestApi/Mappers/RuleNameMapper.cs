using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;


namespace Dot.Net.WebApi.Mappers
{
    public class RuleNameMapper : IMapper<RuleName, RuleNameDTO>
    {
        public RuleNameDTO ToDTO(RuleName entity)
        {
            return new RuleNameDTO
            {
        Id = entity.Id,
        Name = entity.Name,
        Description = entity.Description,
        Json = entity.Json,
        Template = entity.Template,
        SqlStr = entity.SqlStr,
        SqlPart = entity.SqlPart,
            };
        }

        public RuleName ToEntity(RuleNameDTO dto)
        {
            return new RuleName
            {
 
                Id = dto.Id,
                Name = dto.Name ?? string.Empty,
                Description = dto.Description ?? string.Empty,
                Json = dto.Json ?? string.Empty,
                Template = dto.Template ?? string.Empty,
                SqlStr = dto.SqlStr ?? string.Empty,
                SqlPart = dto.SqlPart ?? string.Empty,
            };
        }
    }
}