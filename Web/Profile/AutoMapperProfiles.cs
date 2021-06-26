
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.Web.Dto.AudioDto;

namespace iread_interaction_ms.Web.Profile
{
    public class AutoMapperProfile:AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            //Audio Mapper
            CreateMap<Audio, AudioDto>().ReverseMap();
            CreateMap<Audio, AudioCreateDto>().ReverseMap();
            CreateMap<Audio, AudioUpdateDto>().ReverseMap();
        }
    }
}