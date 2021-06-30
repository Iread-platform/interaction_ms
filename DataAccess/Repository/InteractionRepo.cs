using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.DataAccess.Repository{
     class InteractionRepo: IInteractionRepo
    {
        private readonly AppDbContext _context;

        public InteractionRepo(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<Interaction> GetById(int id)
        {
        return await _context.Interactions.FindAsync(id);
        }

        public void Update(int id, Interaction interaction)
        {
            _context.Interactions.Update(interaction);
            _context.SaveChanges();
        }

        public async void  Insert(Interaction interaction)  
        {
            await _context.Interactions.AddAsync(interaction);
            await _context.SaveChangesAsync();
        }

        public IEnumerable<Interaction>  Get()
        {
            return _context.Interactions.AsEnumerable();
        }

        public void Delete(Interaction interaction)
        {
            _context.Interactions.Remove(interaction);
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.Interactions.Any(interaction => interaction.InteractionId.Equals(id));
        }

        public Task<List<Interaction>> GetByPageId(int pageId)
        {
            return _context.Interactions.Where(i=> i.PageId == pageId).Include(i => i.Comments).ToListAsync();
        }
    }
}