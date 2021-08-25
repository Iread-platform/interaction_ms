
using System;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.Web.Dto.AudioDto;
using iread_interaction_ms.Web.Dto.CommentDto;
using iread_interaction_ms.Web.Dto.InteractioDto;
using iread_interaction_ms.Web.Dto.ReadingDto;

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
            CreateMap<Drawing, DrawingDto>().ReverseMap()
            .ForMember(dest => dest.Max_X, opt => opt.MapFrom(src => src.MaxX))
            .ForMember(dest => dest.Max_Y, opt => opt.MapFrom(src => src.MaxY))
            .ForMember(dest => dest.Min_X, opt => opt.MapFrom(src => src.MinX))
            .ForMember(dest => dest.Min_Y, opt => opt.MapFrom(src => src.MinY));

            CreateMap<Drawing, DrawingCreateDto>().ReverseMap()
           .ForMember(dest => dest.Max_X, opt => opt.MapFrom(src => src.MaxX))
           .ForMember(dest => dest.Max_Y, opt => opt.MapFrom(src => src.MaxY))
           .ForMember(dest => dest.Min_X, opt => opt.MapFrom(src => src.MinX))
           .ForMember(dest => dest.Min_Y, opt => opt.MapFrom(src => src.MinY));


            CreateMap<Drawing, DrawingWithAudioDto>()
            .ForMember(dest => dest.MaxX, opt => opt.MapFrom(src => src.Max_X))
            .ForMember(dest => dest.MaxY, opt => opt.MapFrom(src => src.Max_Y))
            .ForMember(dest => dest.MinX, opt => opt.MapFrom(src => src.Min_X))
            .ForMember(dest => dest.MinY, opt => opt.MapFrom(src => src.Min_Y));

            CreateMap<DrawingUpdateDto, Drawing>().ForMember(dest =>
            dest.AudioId,
            opt => opt.MapFrom(src => src.AudioId == null ? 0 : src.AudioId));


            //HighLight Mapper
            CreateMap<HighLight, HighLightDto>().ReverseMap();
            CreateMap<HighLightCreateDto, HighLight>().ReverseMap();
            CreateMap<HighLightUpdateDto, HighLight>().ReverseMap();

            //Reading Mapper
            CreateMap<Reading, ReadingDto>().ReverseMap().ForMember(dest =>
            dest.TimeSpent,
            opt => opt.MapFrom(src => src.TimeSpent.ToString()));
            CreateMap<ReadingCreateDto, Reading>().ForMember(dest =>
            dest.TimeSpent,
            opt => opt.MapFrom(src => TimeSpan.Parse(src.TimeSpent)));
            CreateMap<Reading, ReadingWithProgressDto>().ReverseMap();


        }
    }
}