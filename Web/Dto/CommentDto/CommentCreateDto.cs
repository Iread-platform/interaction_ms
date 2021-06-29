using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Dto.InteractioDto;

namespace iread_interaction_ms.Web.Dto.CommentDto
{
    public class CommentCreateDto
    {
        public string CommentType { get; set; }
        public string Value { get; set; }
        public InteractionCreateDto interaction {get; set; }
        
        [Required]
        [RegularExpression("([0-9]{13})?$", ErrorMessage = "The {0} must be numeric and with milliseconds (13 digits)")]
        public string WordTimesTamp { get; set; }
        
    }
}