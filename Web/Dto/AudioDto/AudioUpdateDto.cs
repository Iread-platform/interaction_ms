using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Util;

namespace iread_interaction_ms.Web.Dto.AudioDto
{
    public class AudioUpdateDto
    {
        [Required(ErrorMessage = ErrorMessage.Audio_ID_IS_REQUIRED)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.INVALID_Audio_ID_VALUE)]
        public int AudioId { get; set; }
        
        [Required(ErrorMessage = ErrorMessage.Attachment_ID_IS_REQUIRED)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.INVALID_Attachment_ID_VALUE)]
        public int AttachmentId { get; set; }
    }
}