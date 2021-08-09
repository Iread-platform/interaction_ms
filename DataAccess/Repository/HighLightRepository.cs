using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.DataAccess.Repository
{
    public class HighLightRepository : IHighLightRepository
    {
        private readonly AppDbContext _context;

        public HighLightRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<HighLight> GetById(int id)
        {
            return await _context.HighLights.Include(h => h.Interaction).FirstOrDefaultAsync(h => h.HighLightId == id);
        }

        public async Task<HighLight> GetByInteractionId(int id)
        {
            return await _context.HighLights.Include(h => h.Interaction).FirstOrDefaultAsync(h => h.InteractionId == id);
        }

        public void Insert(HighLight highLight)
        {
            _context.HighLights.Add(highLight);
            _context.SaveChanges();
        }

        public void Delete(HighLight highLight)
        {
            _context.HighLights.Remove(highLight);
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.HighLights.Any(h => h.HighLightId == id);
        }

        public void Update(HighLight highLight, HighLight oldHighLight)
        {
            _context.Entry(oldHighLight).State = EntityState.Deleted;
            _context.HighLights.Attach(highLight);
            _context.Entry(highLight).State = EntityState.Modified;
            _context.Entry(highLight).Reference(h => h.Interaction).IsModified = false;
            _context.Entry(highLight).Property(h => h.InteractionId).IsModified = false;
            _context.SaveChanges();
        }

        public async Task<List<HighLight>> GetByPageId(int pageId)
        {
            return await _context.HighLights.Include(a => a.Interaction).Where(a => a.Interaction.PageId == pageId).ToListAsync();
        }
    }
}
