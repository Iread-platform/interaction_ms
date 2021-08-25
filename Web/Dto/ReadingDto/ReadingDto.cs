
using System;
using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Dto.InteractioDto;
using iread_interaction_ms.Web.Util;

namespace iread_interaction_ms.Web.Dto.ReadingDto
{
    public class ReadingDto
    {
        public int ReadingId { get; set; }

        public InnerInteractionDto Interaction { get; set; }
        public string TimeSpent { get; set; }
        public DateTime StartDate { get; set; }

    }
}