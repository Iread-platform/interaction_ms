using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using iread_interaction_ms.Web.Dto.CommentDto;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.Web.Service
{
    public class CommentsService
    {
        private readonly IPublicRepository _publicRepository;

        public CommentsService(IPublicRepository publicRepository)
        {
            _publicRepository = publicRepository;
        }

        public async Task<Comment> GetById(int id)
        {
            return await _publicRepository.GetCommentsRepo.GetById(id);
        }

        public async Task<Comment> GetByInteractionId(int id)
        {
            return await _publicRepository.GetCommentsRepo.GetByInteractionId(id);
        }

        public bool Insert(Comment comment)
        {
            try
            {
                _publicRepository.GetCommentsRepo.Insert(comment);
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        public bool Exists(int id)
        {
            return _publicRepository.GetCommentsRepo.Exists(id);
        }

        internal void Update(Comment comment, Comment oldComment)
        {
            _publicRepository.GetCommentsRepo.Update(comment, oldComment);
        }

        internal void Delete(Comment comment)
        {
            _publicRepository.GetInteractionRepo.Delete(comment.Interaction);
        }

        public async Task<List<Comment>> GetByPageId(int pageId)
        {
            return await _publicRepository.GetCommentsRepo.GetByPageId(pageId);
        }
    }
}