namespace TicTacToe.CustomException
{
    [Serializable]
    public class UserRequestExitException : Exception
    {
        public UserRequestExitException()
        {
        }

        public UserRequestExitException(string? message) : base(message)
        {
        }

        public UserRequestExitException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UserRequestExitException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext) :base(serializationInfo, streamingContext) 
        {
            
        }
    }
}
