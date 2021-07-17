using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.Web.Service
{
    public class AudioService
    {
        private readonly IPublicRepository _publicRepository;

        public AudioService(IPublicRepository publicRepository)
        {
            _publicRepository = publicRepository;
        }

        public async Task<Audio> GetById(int id)
        {
            return await _publicRepository.GetAudioRepo.GetById(id);
        }

        public bool Insert(Audio audio)
        {
            try
            {
                _publicRepository.GetAudioRepo.Insert(audio);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<bool> HasAudio(int  interactionId)
        {
            return await _publicRepository.GetAudioRepo.HasAudio(interactionId);
        }

        public async Task<Audio> GetByInteractionId(int id)
        {
            return await _publicRepository.GetAudioRepo.GetByInteractionId(id);
        }

        public void Delete(Audio audio)
        {
            _publicRepository.GetInteractionRepo.Delete(audio.Interaction);
        }
    }
}