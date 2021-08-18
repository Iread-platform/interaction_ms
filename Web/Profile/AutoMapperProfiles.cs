
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.Web.Dto.AudioDto;
using iread_interaction_ms.Web.Dto.CommentDto;
using iread_interaction_ms.Web.Dto.InteractioDto;

namespace iread_interaction_ms.Web.Profile
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            //Audio Mapper
            CreateMap<Audio, AudioDto>().ReverseMap();
            CreateMap<Audio, AudioCreateDto>().ReverseMap();
            CreateMap<Audio, AudioUpdateDto>().ReverseMap();

            //Interaction Mapper
            CreateMap<InteractionCreateDto, Interaction>().ReverseMap();
            CreateMap<Interaction, InnerInteractionDto>().ReverseMap();
            CreateMap<Interaction, InteractionDto>()
        .ForMember(dest =>
            dest.CommentId,
            opt => opt.MapFrom(src => src.Comments.Count > 0 ? src.Comments[0].CommentId : 0))
        .ForMember(dest =>
            dest.CommentType,
            opt => opt.MapFrom(src => src.Comments.Count > 0 ? src.Comments[0].CommentType : null))
        .ForMember(dest =>
            dest.Value,
            opt => opt.MapFrom(src => src.Comments.Count > 0 ? src.Comments[0].Value : null))
        .ForMember(dest =>
            dest.WordTimesTamp,
            opt => opt.MapFrom(src => src.Comments.Count > 0 ? src.Comments[0].WordTimesTamp : null))
        .ForMember(dest =>
            dest.Word,
            opt => opt.MapFrom(src => src.Comments.Count > 0 ? src.Comments[0].Word : null));

            //Comment Mapper
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<CommentCreateDto, Comment>().ReverseMap();
            CreateMap<CommentUpdateDto, Comment>().ReverseMap();


            //Drawing Mapper
            CreateMap<Drawing, DrawingDto>().ReverseMap();
            CreateMap<DrawingCreateDto, Drawing>().ReverseMap();
            CreateMap<Drawing, DrawingWithAudioDto>().ReverseMap().ForMember(dest =>
            dest.AudioId,
            opt => opt.MapFrom(src => src.Audio == null));


            CreateMap<DrawingUpdateDto, Drawing>().ForMember(dest =>
            dest.AudioId,
            opt => opt.MapFrom(src => src.AudioId == null ? 0 : src.AudioId));


            //HighLight Mapper
            CreateMap<HighLight, HighLightDto>().ReverseMap();
            CreateMap<HighLightCreateDto, HighLight>().ReverseMap();
            CreateMap<HighLightUpdateDto, HighLight>().ReverseMap();

        }
    }
}