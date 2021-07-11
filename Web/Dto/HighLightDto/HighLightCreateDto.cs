using System;
using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Dto.InteractioDto;
using M3allem.M3allem.Utils;

namespace iread_interaction_ms.Web.Dto.CommentDto
{
    public class HighLightCreateDto
    {
        [Required]
        public InteractionCreateDto interaction {get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} can't be negative.")]
        public Nullable<int> FirstWordIndex { get; set; }
        
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} can't be negative.")]
        [BiggerThan(ComparedProperty = "FirstWordIndex", ErrorMessage = "The {0} must be bigger than FirstWordIndex")]
        public Nullable<int> EndWordIndex { get; set; }

        
        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "The {0} must be one word only")]
        public string FirstWord { get; set; }

        
        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "The {0} must be one word only")]
        public string EndWord { get; set; }
        
    }
}