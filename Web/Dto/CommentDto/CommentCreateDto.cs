using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Dto.InteractioDto;

namespace iread_interaction_ms.Web.Dto.CommentDto
{
    public class CommentCreateDto
    {
        [Required(AllowEmptyStrings = false)]
        [EnumDataType(typeof(CommentType), ErrorMessage = "The {0} doesn't exist within enum should be one of [EXAMPLE, WORD_CLASS]")]
        public string CommentType { get; set; }
        
        [Required]
        public string Value { get; set; }
        [Required]
        public InteractionCreateDto interaction {get; set; }
        
        [Required]
        [RegularExpression("([0-9]{13})?$", ErrorMessage = "The {0} must be numeric and with milliseconds (13 digits)")]
        public string WordTimesTamp { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "The {0} must be one word only")]
        public string Word { get; set; }
        
    }
}