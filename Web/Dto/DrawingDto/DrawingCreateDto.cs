using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Dto.InteractioDto;

namespace iread_interaction_ms.Web.Dto.CommentDto
{
    public class DrawingCreateDto
    {
        
        [Required(AllowEmptyStrings = false)]
        public string Points { get; set; }
        
        [Required]
        public InteractionCreateDto interaction {get; set; }
        
        public int AudioId { get; set; }

        public string Comment { get; set; }
        
    }
}