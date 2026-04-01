using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public class RuleName : BaseEntity
    {

        //[Required(ErrorMessage = "Le nom est obligatoire.")]
        public string? Name { get; set; }

        //[Required(ErrorMessage = "La description est obligatoire.")]
        public string? Description { get; set; }

        public string? Json { get; set; }
        public string? Template { get; set; }
        public string? SqlStr { get; set; }
        public string? SqlPart { get; set; }
    }
}