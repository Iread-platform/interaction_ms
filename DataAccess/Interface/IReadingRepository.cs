using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.Web.Dto.ReadingDto;

namespace iread_interaction_ms.DataAccess.Interface
{
    public interface IReadingRepository
    {
        public Task<Reading> GetById(int id);

        public void Insert(Reading reading);

        public void Delete(Reading reading);

        public bool Exists(int id);

        public void Update(Reading reading, Reading oldReading);

        public Task<Reading> GetByInteractionId(int id);
        public Task<List<Reading>> GetByPageId(int pageId);
        public Task<List<ReadingWithProgressDto>> GetCountOfReadedPagesForEachMyStory(string studentId);
    }
}