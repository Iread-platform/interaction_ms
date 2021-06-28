using System.Linq;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.DataAccess.Repository
{
    public class CommentRepository :ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Comment> GetById(int id)
        {
            return await _context.Comments.Include(c => c.Interaction).FirstOrDefaultAsync(c => c.CommentId == id);
        }

        public async Task<Comment> GetByInteractionId(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(c => c.InteractionId == id);
        }

        public void Insert(Comment comment)
        {
            _context.Comments.Add(comment);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var toRemove = new Comment() { CommentId = id };
            _context.Comments.Attach(toRemove);
            _context.Comments.Remove(toRemove);
            _context.SaveChangesAsync();
        }

        public bool Exists(int id)
        {
            return _context.Comments.Any(c => c.CommentId == id);
        }

        public void Update(int id, Comment comment, Comment oldComment)
        {
            _context.Entry(oldComment).State = EntityState.Modified;
            _context.Comments.Attach(oldComment);
            _context.Update(oldComment);
            _context.SaveChanges();
        }

      
    }
}