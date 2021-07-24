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
        
        public Nullable<int> FirstWordIndex { get; set; }
        
        public Nullable<int> EndWordIndex { get; set; }

        public string FirstWord { get; set; }

        public string EndWord { get; set; }
        
        [Required]
        public  Nullable<int> AttachmentId { get; set; }
    
    }
}