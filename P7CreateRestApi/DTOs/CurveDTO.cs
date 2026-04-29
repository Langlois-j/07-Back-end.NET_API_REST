using Dot.Net.WebApi.Validators;
using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.DTOs
{
    public class CurveDTO
    {
        public int Id { get; set; }
        [Required]
        [Byte_Valid]
        public byte? CurveId { get; set; }
     
        public double? Term { get; set; }
        public double? CurvePointValue { get; set; }
    }
}
