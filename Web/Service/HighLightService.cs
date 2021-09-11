using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using iread_interaction_ms.Web.Dto.CommentDto;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.Web.Service
{
    public class HighLightService
    {
        private readonly IPublicRepository _publicRepository;

        public HighLightService(IPublicRepository publicRepository)
        {
            _publicRepository = publicRepository;
        }

        public async Task<HighLight> GetById(int id)
        {
            return await _publicRepository.GetHighLightRepo.GetById(id);
        }

        public async Task<HighLight> GetByInteractionId(int id)
        {
            return await _publicRepository.GetHighLightRepo.GetByInteractionId(id);
        }

        public bool Insert(HighLight highLight)
        {
            try
            {
                _publicRepository.GetHighLightRepo.Insert(highLight);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public bool Exists(int id)
        {
            return _publicRepository.GetHighLightRepo.Exists(id);
        }

        internal void Update(HighLight highLight, HighLight oldHighLight)
        {
            _publicRepository.GetHighLightRepo.Update(highLight, oldHighLight);
        }

        internal void Delete(HighLight highLight)
        {
            _publicRepository.GetInteractionRepo.Delete(highLight.Interaction);
        }

        public async Task<List<HighLight>> GetByPageId(int pageId)
        {
            return await _publicRepository.GetHighLightRepo.GetByPageId(pageId);
        }

        internal async Task<List<List<HighLight>>> GetByPagesIds(List<int> pagesIdsAsIntlist)
        {
            List<List<HighLight>> res = new List<List<HighLight>>();

            foreach (int pageId in pagesIdsAsIntlist)
            {
                res.Add(await _publicRepository.GetHighLightRepo.GetByPageId(pageId));
            }
            return res;
        }
    }
}