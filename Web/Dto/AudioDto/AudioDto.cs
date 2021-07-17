using System;
using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Dto.InteractioDto;
using iread_interaction_ms.Web.Util;

namespace iread_interaction_ms.Web.Dto.AudioDto
{
    public class AudioDto
    {
        [Required(ErrorMessage = ErrorMessage.Interaction_ID_IS_REQUIRED)]
        [Range(1, int.MaxValue, ErrorMessage = ErrorMessage.INVALID_Interaction_ID_VALUE)]
        public int AudioId { get; set; }
        
        public InnerInteractionDto Interaction { get; set; }
        
        public int AttachmentId { get; set; }

        public Nullable<int> FirstWordIndex { get; set; }
        public Nullable<int> EndWordIndex { get; set; }
        public string FirstWord { get; set; }
        public string EndWord { get; set; }

    }
}