
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
        public double MaxX { get; set; }
        public double MaxY { get; set; }
        public double MinX { get; set; }
        public double MinY { get; set; }
        public string Color { get; set; }

    }
}