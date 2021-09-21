using System;
using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Util;

namespace iread_interaction_ms.Web.Dto.AudioDto
{
    public class AudioUpdateDto
    {
        
        [Required]
        public  Nullable<int> AttachmentId { get; set; }
    }
}