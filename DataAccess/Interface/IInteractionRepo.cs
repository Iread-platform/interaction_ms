using System.Collections.Generic;
using iread_interaction_ms.DataAccess.Data.Entity;

namespace iread_interaction_ms.DataAccess.Interface
{
    public interface IInteractionRepo
    {
        Interaction Get(int id);

        void Update(int id, Interaction interaction);
       
        void Add(Interaction interaction);

        IEnumerable<Interaction> Get();

        void Delete(int id);

        bool Exists(int id);

    }
}