using System;
using System.ComponentModel.DataAnnotations;

namespace iread_interaction_ms.Web.Dto.CommentDto
{
    public class DrawingUpdateDto
    {
        public string Comment { get; set; }
        public Nullable<int> AudioId { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Color { get; set; }

    }
}