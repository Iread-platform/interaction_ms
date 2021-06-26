using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.Web.Service
{
    public class AudioServices
    {
        private readonly IPublicRepository _publicRepository;

        public AudioServices(IPublicRepository publicRepository)
        {
            _publicRepository = publicRepository;
        }

        public async Task<Audio> GetAudioByID(int id)
        {
            return await _publicRepository.GetAudioRepository.GetById(id);
        }

        public bool InsertAudio(Audio audio)
        {
            try
            {
                _publicRepository.GetAudioRepository.Insert(audio);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> IsInteractionHasAudio(int  interactionId)
        {
            return await _publicRepository.GetAudioRepository.IsSInteractionHasAudio(interactionId);
        }
    }
}