using System;
using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Dto.InteractioDto;
using M3allem.M3allem.Utils;

namespace iread_interaction_ms.Web.Dto.ReadingDto
{
    public class ReadingCreateDto
    {
        [Required]
        public InteractionCreateDto interaction { get; set; }

        [Required]
        public string TimeSpent { get; set; }
        [Required]
        public DateTime StartDate { get; set; }

    }
}