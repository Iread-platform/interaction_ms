using System;
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

        public CommentRepository()
        {
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

        public void Delete(Comment comment)
        {
            _context.Comments.Remove(comment);
            _context.Interactions.Remove(comment.Interaction);
            _context.SaveChanges();
        }

        public bool Exists(int id)
        {
            return _context.Comments.Any(c => c.CommentId == id);
        }

        public void Update(Comment comment, Comment oldComment)
        {
            _context.Entry(oldComment).State = EntityState.Deleted;
            _context.Comments.Attach(comment);
            _context.Entry(comment).State = EntityState.Modified;
            _context.Entry(comment).Reference(c => c.Interaction).IsModified = false;
            _context.Entry(comment).Property(c => c.InteractionId).IsModified = false;
            _context.Entry(comment).Property(c => c.Word).IsModified = false;
            _context.Entry(comment).Property(c => c.WordTimesTamp).IsModified = false;
            _context.SaveChanges();

        }
    }            
}
