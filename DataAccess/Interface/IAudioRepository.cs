using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;

namespace iread_interaction_ms.DataAccess.Interface
{
    public interface IAudioRepository
    {
        public Task<Audio> GetById(int id);
        
        public void Insert(Audio audio);
        
        public void Delete(int id);

        public bool Exists(int id);

        public Task<Audio> GetByInteractionId(int id);

        public Task<bool> HasAudio(int  interactionId);

        public void Update(Audio audioEntity, Audio oldAudio);
    }
}