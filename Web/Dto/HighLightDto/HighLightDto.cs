
using System;
using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Dto.InteractioDto;
using iread_interaction_ms.Web.Util;

namespace iread_interaction_ms.Web.Dto.CommentDto
{
    public class HighLightDto
    {
        public int HighLightId { get; set; }
        
        public InnerInteractionDto Interaction { get; set; }
        
        public int FirstWordIndex { get; set; }
        
        public int EndWordIndex { get; set; }
        
        public string FirstWord { get; set; }
        
        public string EndWord { get; set; }
        
    }
}