using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.DataAccess.Repository
{
    public class AudioRepository : IAudioRepository
    {
        private readonly AppDbContext _context;

        public AudioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Audio> GetById(int id)
        {
            return await _context.Audios.Include(a => a.Interaction).FirstOrDefaultAsync(a => a.AudioId == id);
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
        public bool Exists(int pageId , int storyId, string userId)
        {
            return _context.Audios.Include(a => a.Interaction).Any(a =>
                a.Interaction.PageId == pageId && a.Interaction.StoryId == storyId &&
                a.Interaction.StudentId == userId);
        }

        public void Update(Audio audio, Audio oldAudio)
        {
            _context.Entry(oldAudio).State = EntityState.Deleted;
            _context.Audios.Attach(audio);
            _context.Entry(audio).State = EntityState.Modified;
            _context.Entry(audio).Reference(c => c.Interaction).IsModified = false;
            _context.Entry(audio).Property(c => c.InteractionId).IsModified = false;
            _context.SaveChanges();
        }

        public async Task<bool> HasAudio(int interactionId)
        {
            return _context.Audios.Any(a => a.InteractionId == interactionId);
        }

        public async Task<Audio> GetByInteractionId(int id)
        {
            return await _context.Audios.Include(a => a.Interaction).FirstOrDefaultAsync(a => a.InteractionId == id);
        }

        public async Task<List<Audio>> GetByPageId(int pageId)
        {
            return await _context.Audios.Include(a => a.Interaction).Where(a => a.Interaction.PageId == pageId).ToListAsync();

        }
    }
}