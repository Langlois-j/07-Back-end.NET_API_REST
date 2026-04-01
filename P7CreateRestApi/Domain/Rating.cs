using Dot.Net.WebApi.Validators;
using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class Rating : BaseEntity
    {
       
        //[Required(ErrorMessage = "Le rating Moody's est obligatoire.")]
        public string? MoodysRating { get; set; }

        //[Required(ErrorMessage = "Le rating S&P est obligatoire.")]
        public string? SandPRating { get; set; }

        //[Required(ErrorMessage = "Le rating Fitch est obligatoire.")]
        public string? FitchRating { get; set; }

        [ValidByte]
        public byte? OrderNumber { get; set; }
    }
}