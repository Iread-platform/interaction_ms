
using System;
using iread_interaction_ms.Web.Dto.InteractioDto;
using iread_interaction_ms.Web.DTO;

namespace iread_interaction_ms.Web.Dto.CommentDto
{
    public class DrawingWithAudioDto
    {
        public int DrawingId { get; set; }
        public string Comment { get; set; }
        public AttachmentDTO Audio { get; set; } = null;
        public InnerInteractionDto Interaction { get; set; }
        public string Points { get; set; }
        public Nullable<int> MaxX { get; set; }
        public Nullable<int> MaxY { get; set; }
        public Nullable<int> MinX { get; set; }
        public Nullable<int> MinY { get; set; }
        public string Color { get; set; }

    }
}