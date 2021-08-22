using System;
using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Dto.InteractioDto;

namespace iread_interaction_ms.Web.Dto.CommentDto
{
    public class DrawingCreateDto
    {

        [Required(AllowEmptyStrings = false)]
        public string Points { get; set; }

        [Required]
        public InteractionCreateDto interaction { get; set; }

        public int AudioId { get; set; }

        public string Comment { get; set; }

        [Required]
        public Nullable<double> MaxX { get; set; }
        [Required]
        public Nullable<double> MaxY { get; set; }
        [Required]
        public Nullable<double> MinX { get; set; }
        [Required]
        public Nullable<double> MinY { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Color { get; set; }

    }
}