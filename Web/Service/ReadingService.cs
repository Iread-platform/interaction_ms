using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using iread_interaction_ms.Web.Dto.ReadingDto;
using iread_interaction_ms.Web.DTO.StoryDto;
using Microsoft.EntityFrameworkCore;

namespace iread_interaction_ms.Web.Service
{
    public class ReadingService
    {
        private readonly IPublicRepository _publicRepository;
        private readonly IMapper _mapper;

        public ReadingService(IPublicRepository publicRepository, IMapper mapper)
        {
            _publicRepository = publicRepository;
            _mapper = mapper;
        }

        public async Task<Reading> GetById(int id)
        {
            return await _publicRepository.GetReadingRepo.GetById(id);
        }

        public async Task<Reading> GetByInteractionId(int id)
        {
            return await _publicRepository.GetReadingRepo.GetByInteractionId(id);
        }

        public void Insert(Reading reading)
        {
            _publicRepository.GetReadingRepo.Insert(reading);
        }

        public bool Exists(int id)
        {
            return _publicRepository.GetReadingRepo.Exists(id);
        }

        internal void Update(Reading reading, Reading oldReading)
        {
            _publicRepository.GetReadingRepo.Update(reading, oldReading);
        }

        internal void Delete(Reading reading)
        {
            _publicRepository.GetInteractionRepo.Delete(reading.Interaction);
        }

        public async Task<List<Reading>> GetByPageId(int pageId)
        {
            return await _publicRepository.GetReadingRepo.GetByPageId(pageId);
        }

        internal async Task<List<ReadingWithProgressDto>> GetMyReadingStoriesAndProgress(string studentId)
        {

            return await _publicRepository.GetReadingRepo.GetCountOfReadedPagesForEachMyStory(studentId);
        }

        internal async Task<List<StoryDto>> GetReadingStoryIds(string studentId)
        {
            return await _publicRepository.GetReadingRepo.GetReadingStoryIds(studentId);
        }
    }
}