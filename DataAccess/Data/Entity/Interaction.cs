using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iread_interaction_ms.DataAccess.Data.Entity
{
    [Table("Interactions")]
    public class Interaction
    {
        

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int InteractionId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The {0} field is required.")]
        public int StoryId { get; set; }
        
        [Required]
        public string StudentId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The {0} field is required.")]
        public int PageId { get; set; }

        public List<Comment> Comments;

        public List<Drawing> Drawings;
    }
}
