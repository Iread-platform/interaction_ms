using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Util;

namespace iread_interaction_ms.Web.Dto.AudioDto
{
    public class AudioCreateDto
    {
        [Required(ErrorMessage = ErrorMessage.Interaction_ID_IS_REQUIRED)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.INVALID_Interaction_ID_VALUE)]
        public int InteractionId { get; set; }
        
        [Required(ErrorMessage = ErrorMessage.Attachment_ID_IS_REQUIRED)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.INVALID_Attachment_ID_VALUE)]
        public int AttachmentId { get; set; }
    }
}