using iread_interaction_ms.Web.Dto.InteractioDto;

namespace iread_interaction_ms.Web.Dto.CommentDto
{
    public class InnerHighLightDto
    {
        public int HighLightId { get; set; }

        public int FirstWordIndex { get; set; }

        public int EndWordIndex { get; set; }

        public string FirstWord { get; set; }

        public string EndWord { get; set; }

    }
}