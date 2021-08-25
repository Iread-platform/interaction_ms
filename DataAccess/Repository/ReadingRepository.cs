using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using iread_interaction_ms.Web.Dto.ReadingDto;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.DataAccess.Repository
{
    public class ReadingRepository : IReadingRepository
    {
        private readonly AppDbContext _context;

        public ReadingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Reading> GetById(int id)
        {
            return await _context.Readings.Include(h => h.Interaction).FirstOrDefaultAsync(r => r.ReadingId == id);
        }

        public async Task<Reading> GetByInteractionId(int id)
        {
            return await _context.Readings.Include(h => h.Interaction).FirstOrDefaultAsync(r => r.InteractionId == id);
        }

        public void Insert(Reading reading)
        {
            _context.Readings.Add(reading);
            _context.SaveChanges();
        }

        public void Delete(Reading reading)
        {
            _context.Readings.Remove(reading);
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.Readings.Any(r => r.ReadingId == id);
        }

        public void Update(Reading reading, Reading oldReading)
        {
            _context.Entry(oldReading).State = EntityState.Deleted;
            _context.Readings.Attach(reading);
            _context.Entry(reading).State = EntityState.Modified;
            _context.Entry(reading).Reference(h => h.Interaction).IsModified = false;
            _context.Entry(reading).Property(h => h.InteractionId).IsModified = false;
            _context.SaveChanges();
        }

        public async Task<List<Reading>> GetByPageId(int pageId)
        {
            return await _context.Readings.Include(a => a.Interaction).Where(a => a.Interaction.PageId == pageId).ToListAsync();
        }


        public async Task<List<ReadingWithProgressDto>> GetCountOfReadedPagesForEachMyStory(string studentId)
        {

            return await _context.Readings
                    .Include(r => r.Interaction)
                    .Where(r => r.Interaction.StudentId == studentId)
                    .GroupBy(r => r.Interaction.StoryId)
                    .Select(r => new ReadingWithProgressDto { StoryId = r.Key, Count = r.Count() })
                    .ToListAsync();
        }

    }
}
