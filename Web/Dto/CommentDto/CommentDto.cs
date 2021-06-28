
using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Dto.InteractioDto;
using iread_interaction_ms.Web.Util;

namespace iread_interaction_ms.Web.Dto.CommentDto
{
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string CommentType { get; set; }
        public string Value { get; set; }
        public InteractionDto Interaction { get; set; }
        
    }
}