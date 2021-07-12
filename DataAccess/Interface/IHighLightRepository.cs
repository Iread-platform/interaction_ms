using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;

namespace iread_interaction_ms.DataAccess.Interface
{
    public interface IHighLightRepository
    {
        public Task<HighLight> GetById(int id);
        
        public void Insert(HighLight highLight);
        
        public void Delete(HighLight highLight);

        public bool Exists(int id);

        public void Update(HighLight highLight, HighLight oldHighLight);
        
        public Task<HighLight> GetByInteractionId(int id);
    }
}