using System.Collections.Generic;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;

namespace iread_interaction_ms.DataAccess.Interface
{
    public interface IInteractionRepo
    {
         Task<Interaction> GetById(int id);

        void Update(int id, Interaction interaction);
       
        void Insert(Interaction interaction);

        IEnumerable<Interaction> Get();

        public void Delete(Interaction interaction);

        bool Exists(int id);
        Task<List<Interaction>> GetByPageId(int pageId);
    }
}