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

        public async Task<Audio> GetAudioById(int id)
        {
            return await _publicRepository.GetAudioRepo.GetById(id);
        }

        public bool InsertAudio(Audio audio)
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
    }
}