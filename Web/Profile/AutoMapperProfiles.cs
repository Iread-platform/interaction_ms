
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.Web.Dto.AudioDto;
using iread_interaction_ms.Web.Dto.CommentDto;
using iread_interaction_ms.Web.Dto.InteractioDto;

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
            
            //Interaction Mapper
            CreateMap<Interaction, InteractionDto>().ReverseMap();
            CreateMap<InteractionCreateDto, Interaction>().ReverseMap();

            //Audio Mapper
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<CommentCreateDto, Comment>().ReverseMap();
            CreateMap<CommentUpdateDto, Comment>().ReverseMap();
           
            
        }
    }
}