
namespace iread_interaction_ms.DataAccess.Interface
{
    public interface IPublicRepository
    {
        IAudioRepository GetAudioRepo { get; }
        IInteractionRepo GetInteractionRepo { get; }
        ICommentRepository GetCommentsRepo { get; }
        IDrawingRepository GetDrawingRepo { get; }
        IHighLightRepository GetHighLightRepo { get; }
        IReadingRepository GetReadingRepo { get; }


    }
}