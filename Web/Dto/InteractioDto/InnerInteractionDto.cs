using System.Collections.Generic;
using iread_interaction_ms.Web.Dto.CommentDto;

namespace iread_interaction_ms.Web.Dto.InteractioDto
{
    public class InnerInteractionDto
    {
        public int InteractionId { get; set; }

        public int StoryId { get; set; }
        
        public string StudentId { get; set; }
        
        public int PageId { get; set; }
        
        
    }
}