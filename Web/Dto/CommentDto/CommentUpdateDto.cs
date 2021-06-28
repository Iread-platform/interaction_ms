using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Dto.InteractioDto;
using iread_interaction_ms.Web.Util;

namespace iread_interaction_ms.Web.Dto.CommentDto
{
    public class CommentUpdateDto
    {
        
        [Required]
        public string Value { get; set; }
        
        [Required]
        [EnumDataType(typeof(CommentType), ErrorMessage = "The {0} doesn't exist within enum should be one of [EXAMPLE, WORD_CLASS]")]
         public string CommentType { get; set; }
        
    }
}