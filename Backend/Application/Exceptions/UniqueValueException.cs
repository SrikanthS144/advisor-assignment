using System;
using System.Runtime.Serialization;

namespace Application.Exceptions
{
  [Serializable]
  public class UniqueValueException : Exception
  {
    public UniqueValueException(string name, object key)
      : base($"{name} {key} must be unique")
    {
    }

    public UniqueValueException(string message) : base(message)
    {
    }

    public UniqueValueException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected UniqueValueException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}
