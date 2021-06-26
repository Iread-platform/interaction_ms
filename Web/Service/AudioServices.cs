using iread_interaction_ms.DataAccess.Interface;

namespace iread_interaction_ms.Web.Service
{
    public class AudioServices
    {
        private readonly IAudioRepository _audioRepository;

        public AudioServices(IAudioRepository audioRepository)
        {
            _audioRepository = audioRepository;
        }
    }
}