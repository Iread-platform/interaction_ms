using System.ComponentModel.DataAnnotations;

namespace iread_interaction_ms.Web.Dto.InteractioDto
{
    public class InteractionCreateDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The {0} field is required.")]
        public int StoryId { get; set; }
       

        public string StudentId { get; set; }
        
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The {0} field is required.")]
        public int PageId { get; set; }
    }
}