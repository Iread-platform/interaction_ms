using iread_interaction_ms.Web.Dto.AudioDto;
using iread_interaction_ms.Web.Dto.CommentDto;

namespace iread_interaction_ms.Web.Dto.InteractioDto
{
    public class GeneralInteractionDto
    {
        public int InteractionId { get; set; }
        public int StoryId { get; set; }
        public string StudentId { get; set; }
        public int PageId { get; set; }
        public InnerDrawingDto Drawing { get; set; }
        public InnerCommentDto Comment { get; set; }
        public InnerHighLightDto HighLight { get; set; }
        public InnerAudioDto Audio { get; set; }
    }
}