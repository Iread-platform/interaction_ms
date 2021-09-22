using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.Web.Service
{
    public class InteractionsService
    {
        private readonly IPublicRepository _publicRepository;

        public InteractionsService(IPublicRepository publicRepository)
        {
            _publicRepository = publicRepository;
        }


        public async Task<Interaction> GetById(int id)
        {
            return await _publicRepository.GetInteractionRepo.GetById(id);
        }

        public bool Insert(Interaction interaction)
        {
            try
            {
                _publicRepository.GetInteractionRepo.Insert(interaction);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public async Task<List<Interaction>> GetByPageId(int pageId)
        {
            return await _publicRepository.GetInteractionRepo.GetByPageId(pageId);
        }
    }
}