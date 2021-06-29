using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;

namespace iread_interaction_ms.DataAccess.Interface
{
    public interface ICommentRepository
    {
        public Task<Comment> GetById(int id);
        
        public void Insert(Comment comment);
        
        public void Delete(Comment comment);

        public bool Exists(int id);

        public void Update(Comment comment, Comment oldComment);
        public Task<Comment> GetByInteractionId(int id);
    }
}