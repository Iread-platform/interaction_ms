
using iread_interaction_ms.Web.DTO;

namespace iread_interaction_ms.Web.Dto.ReadingDto
{
    public class ReadingWithProgressDto
    {
        public int StoryId { get; set; }
        public int Count { get; set; }
        public string Title { get; set; }
        public AttachmentDTO StoryCover { get; set; }
        public double Progress { get; set; }
        public int PagesCount { get; set; }

    }
}