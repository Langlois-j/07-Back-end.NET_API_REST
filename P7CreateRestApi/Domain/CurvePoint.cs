using System.ComponentModel.DataAnnotations;
using Dot.Net.WebApi.Validators;

namespace Dot.Net.WebApi.Domain
{
    public class CurvePoint : BaseEntity
    {
      
        [Byte_Valid]
        public byte? CurveId { get; set; }

        [DateTime_FutureAttribute]
        public DateTime? AsOfDate { get; set; }

        [Double_Positive]
        public double? Term { get; set; }

        [Double_Positive]
        public double? CurvePointValue { get; set; }

        [DateTime_Past]
        public DateTime? CreationDate { get; set; }
    }
}