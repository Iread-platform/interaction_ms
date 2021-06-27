using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using iread_interaction_ms.Web.Util;
using Microsoft.AspNetCore.Http;

namespace iread_interaction_ms.Web.Dto.AudioDto
{
    public class AudioCreateDto
    {
        [Required(ErrorMessage = ErrorMessage.Interaction_ID_IS_REQUIRED)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.INVALID_Interaction_ID_VALUE)]
        public int InteractionId { get; set; }
        
        [Required]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".MP3", ".jpg" })]
        public IFormFile Attachment { get; set; }
    }
}