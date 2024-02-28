namespace NMHAssignment.Application.Common.Interfaces
{
    public interface IMessageHub
    {
        void Publish<T>(T data, string queueName);

        string Subscribe<T>(string queueName, Action<T> handleMessage);
    }
}
