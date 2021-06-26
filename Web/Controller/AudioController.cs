using AutoMapper;
using iread_interaction_ms.Web.Service;
using Microsoft.AspNetCore.Mvc;

namespace iread_interaction_ms.Web.Controller
{
    public class AudioController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AudioServices _audioServices;

        public AudioController(AudioServices audioServices, IMapper mapper)
        {
            _audioServices = audioServices;
            _mapper = mapper;
        }
        
        
    }
}