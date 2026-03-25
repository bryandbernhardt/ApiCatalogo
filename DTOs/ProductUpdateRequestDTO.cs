using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs;

public class ProductUpdateRequestDTO : IValidatableObject
{
    [Range(0, 9999, ErrorMessage = "Stock must be between 0 and 9999")]
    public float Stock { get; set; }
    
    public DateTime CreatedAt { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (CreatedAt.TimeOfDay <= DateTime.Now.TimeOfDay)
        {
            yield return new ValidationResult("CreatedAt must be greater than today's date",
                [nameof(this.CreatedAt)]);
        }
    }
}