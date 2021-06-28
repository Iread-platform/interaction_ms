using iread_interaction_ms.Web.Dto.InteractioDto;

namespace iread_interaction_ms.Web.Dto.CommentDto
{
    public class CommentCreateDto
    {
        public string CommentType { get; set; }
        public string Value { get; set; }
        public InteractionCreateDto interaction {get; set; }
        
    }
}