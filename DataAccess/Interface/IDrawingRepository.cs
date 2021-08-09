using System.Collections.Generic;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;

namespace iread_interaction_ms.DataAccess.Interface
{
    public interface IDrawingRepository
    {
        public Task<Drawing> GetById(int id);
        public void Insert(Drawing drawing);
        public void Delete(Drawing drawing);
        public bool Exists(int id);
        public void Update(Drawing drawing, Drawing oldDrawing);
        public Task<Drawing> GetByInteractionId(int id);
        Task<List<Drawing>> GetByPageId(int pageId);
    }
}