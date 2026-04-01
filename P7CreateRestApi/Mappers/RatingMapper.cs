using Dot.Net.WebApi.Domain;
using Dot.Net.WebApi.DTOs;


namespace Dot.Net.WebApi.Mappers
{
    public class RatingMapper : IMapper<Rating, RatingDTO>
    {
        public RatingDTO ToDTO(Rating entity)
        {
            return new RatingDTO
            {
                Id = entity.Id,
        MoodysRating = entity.MoodysRating,
        SandPRating = entity.SandPRating,
        FitchRating = entity.FitchRating,
        OrderNumber = entity.OrderNumber,
            };
        }

        public Rating ToEntity(RatingDTO dto)
        {
            return new Rating
            {
                Id = dto.Id,
                MoodysRating = dto.MoodysRating ?? string.Empty,
                SandPRating = dto.SandPRating ?? string.Empty,
                FitchRating = dto.FitchRating ?? string.Empty,
                OrderNumber = dto.OrderNumber,
            };
        }
    }
}