
using System;
using System.ComponentModel.DataAnnotations;
using iread_interaction_ms.Web.Dto.InteractioDto;
using iread_interaction_ms.Web.Util;

namespace iread_interaction_ms.Web.Dto.CommentDto
{
    public class DrawingDto
    {
        public int DrawingId { get; set; }
        public string Comment { get; set; }
        public string AudioId { get; set; }
        public InnerInteractionDto Interaction { get; set; }
        public string Points { get; set; }
        public Nullable<int> MaxX { get; set; }
        public Nullable<int> MaxY { get; set; }
        public Nullable<int> MinX { get; set; }
        public Nullable<int> MinY { get; set; }
        public string Color { get; set; }


    }
}