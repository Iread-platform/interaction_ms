
namespace iread_interaction_ms.DataAccess.Interface
{
    public interface IPublicRepository
    {
        IAudioRepository GetAudioRepository { get; }
        IInteractionRepo GetInteractionRepo { get; }
        ICommentRepository GetCommentsRepo { get; }
    }
}