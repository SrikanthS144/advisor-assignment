using System;

namespace Application.Exceptions
{
  [Serializable]
  public class NotFoundException : Exception
  {
    public NotFoundException(string name, object key)
      : base($"Entity \"{name}\" ({key}) was not found.") { }

    protected NotFoundException(System.Runtime.Serialization.SerializationInfo serializationInfo, System.Runtime.Serialization.StreamingContext streamingContext)
    {
      throw new NotImplementedException();
    }
  }
}
