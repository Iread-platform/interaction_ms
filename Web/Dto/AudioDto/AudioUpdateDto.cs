using System;
using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Util;

namespace iread_interaction_ms.Web.Dto.AudioDto
{
    public class AudioUpdateDto
    {
        
        public Nullable<int> FirstWordIndex { get; set; }
        
        public Nullable<int> EndWordIndex { get; set; }

        public string FirstWord { get; set; }

        public string EndWord { get; set; }
        
        [Required]
        public  Nullable<int> AttachmentId { get; set; }
    }
}