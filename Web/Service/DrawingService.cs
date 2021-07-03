using System;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using iread_interaction_ms.Web.Dto.CommentDto;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.Web.Service
{
    public class DrawingService
    {
        private readonly IPublicRepository _publicRepository;

        public DrawingService(IPublicRepository publicRepository)
        {
            _publicRepository = publicRepository;
        }

        public async Task<Drawing> GetById(int id)
        {
            return await _publicRepository.GetDrawingRepo.GetById(id);
        }
        
        public async Task<Drawing> GetByInteractionId(int id)
        {
            return await _publicRepository.GetDrawingRepo.GetByInteractionId(id);
        }

        public bool Insert(Drawing drawing)
        {
            try
            {
                _publicRepository.GetDrawingRepo.Insert(drawing);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public bool Exists(int id)
        {
            return _publicRepository.GetDrawingRepo.Exists(id);
        }

        internal void Update(Drawing drawing, Drawing oldDrawing)
        {
            _publicRepository.GetDrawingRepo.Update(drawing, oldDrawing);
        }

        internal void Delete(Drawing drawing)
        {
            _publicRepository.GetInteractionRepo.Delete(drawing.Interaction);
        }
    }
}