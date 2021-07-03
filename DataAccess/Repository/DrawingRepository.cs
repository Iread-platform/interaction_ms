using System;
using System.Linq;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.DataAccess.Repository
{
    public class DrawingRepository :IDrawingRepository
    {
        private readonly AppDbContext _context;

        public DrawingRepository(AppDbContext context)
        {
            _context = context;
        }

        public DrawingRepository()
        {
        }

        public async Task<Drawing> GetById(int id)
        {
            return await _context.Drawings.Include(c => c.Interaction).FirstOrDefaultAsync(d => d.DrawingId == id);
        }

        public async Task<Drawing> GetByInteractionId(int id)
        {
            return await _context.Drawings.FirstOrDefaultAsync(d => d.InteractionId == id);
        }

        public void Insert(Drawing drawing)
        {
            _context.Drawings.Add(drawing);
            _context.SaveChanges();
        }

        public void Delete(Drawing drawing)
        {
            _context.Drawings.Remove(drawing);
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.Drawings.Any(d => d.DrawingId == id);
        }

        public void Update(Drawing drawing, Drawing oldDrawing)
        {
            _context.Entry(oldDrawing).State = EntityState.Deleted;
            _context.Drawings.Attach(drawing);
            _context.Entry(drawing).State = EntityState.Modified;
            _context.Entry(drawing).Reference(d => d.Interaction).IsModified = false;
            _context.Entry(drawing).Property(d => d.InteractionId).IsModified = false;
            _context.Entry(drawing).Property(d => d.Points).IsModified = false;
            _context.SaveChanges();

        }
    }            
}
