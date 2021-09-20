using System;
using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Util;

namespace iread_interaction_ms.Web.Dto.AudioDto
{
    public class AudioUpdateDto
    {
        
        [Required(ErrorMessage = ErrorMessage.PAGE_ID_REQUIRED)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.INVALID_PAGE_ID_VALUE)]
        public int PageId { get; set; }
        
        [Required]
        public  Nullable<int> AttachmentId { get; set; }
    }
}