using iread_interaction_ms.DataAccess.Data;
using iread_interaction_ms.DataAccess.Interface;

namespace iread_interaction_ms.DataAccess.Repository
{
    public class PublicRepository : IPublicRepository
    {
        private readonly AppDbContext _context;
        private IInteractionRepo _interactionRepo;
        private IAudioRepository _audioRepository;
        private ICommentRepository _commentRepository;
        private IHighLightRepository _highLightRepository;
        private IDrawingRepository _drawingRepository;
        private IReadingRepository _readingRepository;



        public PublicRepository(AppDbContext context)
        {
            _context = context;
        }

        public IInteractionRepo GetInteractionRepo
        {
            get
            {
                return _interactionRepo ??= new InteractionRepo(_context);
            }
        }

        public IAudioRepository GetAudioRepo
        {
            get
            {
                return _audioRepository ??= new AudioRepository(_context);
            }
        }
        public ICommentRepository GetCommentsRepo
        {
            get
            {
                return _commentRepository ??= new CommentRepository(_context);
            }
        }

        public IDrawingRepository GetDrawingRepo
        {
            get
            {
                return _drawingRepository ??= new DrawingRepository(_context);
            }
        }
        public IHighLightRepository GetHighLightRepo
        {
            get
            {
                return _highLightRepository ??= new HighLightRepository(_context);
            }
        }

        public IReadingRepository GetReadingRepo
        {
            get
            {
                return _readingRepository ??= new ReadingRepository(_context);
            }
        }
    }
}