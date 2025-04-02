using System.ComponentModel.DataAnnotations;
using FaleMais.Domain.Enums;

namespace FaleMaisApplication.DTOs.Request
{
    public class CallCostRequest : IValidatableObject
    {
        [Required]
        public required string Origin { get; set; }

        [Required]
        public required string Destination { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duração tem que ser maior que 0.")]
        public int Duration { get; set; }

        [EnumDataType(typeof(PlanType), ErrorMessage = "Tipo de plano invalido.")]
        public PlanType PlanType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!Enum.IsDefined(typeof(PlanType), PlanType))
            {
                yield return new ValidationResult("Tipo de plano invalido.", new[] { nameof(PlanType) });
            }
        }

        public override string ToString()
        {
            return $"Origin: {Origin}, Destination: {Destination}, Duration: {Duration}, PlanType: {PlanType}";
        }
    }
}