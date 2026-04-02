using Dot.Net.WebApi.Validators;
using System.ComponentModel.DataAnnotations;

namespace Dot.Net.WebApi.Domain
{
    public abstract class BaseEntity : IEntity
    {
        [Key]
        [Int_Valid(null,0)] 
        public int Id { get; set; }
    }
}