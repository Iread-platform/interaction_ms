﻿using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
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

       
    }
}