using iread_interaction_ms.DataAccess.Data;
using iread_interaction_ms.DataAccess.Interface;

namespace iread_interaction_ms.DataAccess.Repository
{
    public class PublicRepository:IPublicRepository
    {
        private readonly AppDbContext _context;
        private IInteractionRepo _interactionRepo;
        private IAudioRepository _audioRepository;
        private ICommentRepository _commentRepository;

        private IDrawingRepository _drawingRepository;

        

        public PublicRepository(AppDbContext context)
        {
            _context = context;
        }

        public IInteractionRepo GetInteractionRepo {
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

    }
}