using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;

namespace iread_interaction_ms.Web.Service
{
    public class InteractionServices
    {
        private readonly IPublicRepository _publicRepository;

        public InteractionServices(IPublicRepository publicRepository)
        {
            _publicRepository = publicRepository;
        }
        
        public async Task<Interaction> GetInteractionById(int id)
        {
            return await _publicRepository.getInteractionRepo.Get(id);
        }
    }
}