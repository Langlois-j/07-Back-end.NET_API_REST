using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace Dot.Net.WebApi.Domain
{
    public class CurvePoint : BaseEntity
    {
  
        [ValidByte]
        public byte? CurveId { get; set; }

        [ValidDate]
        public DateTime? AsOfDate { get; set; }

        [PositiveDouble]
        public double? Term { get; set; }

        [PositiveDouble]
        public double? CurvePointValue { get; set; }

        [ValidDate]
        public DateTime? CreationDate { get; set; }
    }
}