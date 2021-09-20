using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using iread_interaction_ms.Web.Dto.InteractioDto;
using iread_interaction_ms.Web.Util;
using Microsoft.AspNetCore.Http;

namespace iread_interaction_ms.Web.Dto.AudioDto
{
    public class AudioCreateDto
    {

       [Required]
        public InteractionCreateDto interaction {get; set; }
        
        [Required(ErrorMessage = ErrorMessage.PAGE_ID_REQUIRED)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.INVALID_PAGE_ID_VALUE)]
        public int PageId { get; set; }
        
        [Required]
        public  Nullable<int> AttachmentId { get; set; }
    
    }
}