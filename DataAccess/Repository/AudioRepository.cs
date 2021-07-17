using System.Linq;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.DataAccess.Repository
{
    public class AudioRepository :IAudioRepository
    {
        private readonly AppDbContext _context;

        public AudioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Audio> GetById(int id)
        {
            return await _context.Audios.FindAsync(id);
        }

        public void Insert(Audio audio)
        {
            _context.Audios.Add(audio);
            _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var audioToRemove = new Audio() { AudioId = id };
            _context.Audios.Attach(audioToRemove);
            _context.Audios.Remove(audioToRemove);
            _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _context.Audios.Any(a => a.AudioId == id);
        }

        public void Update(int id, Audio audio, Audio oldAudio)
        {
            _context.Entry(oldAudio).State = EntityState.Modified;
            _context.Audios.Attach(oldAudio);
            oldAudio.AttachmentId = audio.AttachmentId;
            _context.Update(oldAudio);
            _context.SaveChanges();
        }

        public async Task<bool> HasAudio(int  interactionId)
        {
            return _context.Audios.Any(a => a.InteractionId == interactionId);
        }
    }
}