using System;
using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Dto.InteractioDto;
using iread_interaction_ms.Web.Util;

namespace iread_interaction_ms.Web.Dto.AudioDto
{
    public class InnerAudioDto
    {
        public int AudioId { get; set; }

        public int AttachmentId { get; set; }

    }
}