using System.Collections.Generic;

namespace iread_interaction_ms.Web.DTO.StoryDto
{
    public class ReadStoryDto
    {

        public int StoryId { get; set; }
        public AttachmentDTO StoryCover { get; set; }
        public string Title { set; get; }
        public int PagesCount { get; set; }

    }
}