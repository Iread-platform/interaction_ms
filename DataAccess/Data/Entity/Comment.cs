using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iread_interaction_ms.DataAccess.Data.Entity
{
    [Table("Comments")]
    public class Comment
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CommentId { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EnumDataType(typeof(CommentType), ErrorMessage = "Comment type doesn't exist within enum should be one of [EXAMPLE, WORD_CLASS]")]
        public string CommentType { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Value { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The InteractionId field is required.")]
        public int InteractionId { get; set; }
        public Interaction Interaction { get; set; }

        [Required]
        [RegularExpression("([0-9]{13})?$", ErrorMessage = "The {0} must be numeric and with milliseconds (13 digits)")]
        public string WordTimesTamp { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression("^[A-Za-z0-9]+$", ErrorMessage = "The {0} must be one word only")]
        public string Word { get; set; }
        
    }
}
