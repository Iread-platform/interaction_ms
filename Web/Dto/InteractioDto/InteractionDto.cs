using System.Collections.Generic;
using iread_interaction_ms.Web.Dto.CommentDto;

namespace iread_interaction_ms.Web.Dto.InteractioDto
{
    public class InteractionDto
    {
        public int InteractionId { get; set; }

        public int StoryId { get; set; }
        
        public string StudentId { get; set; }
        
        public int PageId { get; set; }
        
        public int CommentId { get; set; }
        public string CommentType { get; set; }
        public string Value { get; set; }
        public string WordTimesTamp { get; set; }
        public string Word { get; set; }
        
    }
}