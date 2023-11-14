using System.Xml.Serialization;

namespace We.Ping.Smart;

internal static class StringExtensions
{
    internal static Result<T> FromXml<T>(this string value)
    {
        if(string.IsNullOrEmpty(value))
            return Result.Failure<T>($"The value to deserialize is null or empty to deserialize {typeof(T)}");
        using TextReader reader = new StringReader(value);
        var ser= new XmlSerializer(typeof(T));
        var res=ser.Deserialize(reader);
        if (res == null)
            return Result.Failure<T>( $"Nothing to deserialize {typeof(T)}");
        var result =(T) res ;
        if(result==null)
            return Result.Failure<T>($"Cannot deserialize to type {typeof(T)}");

        return Result.Success(result) ;
    }

    internal static bool IsNotNullNorEmpty(this string value)=>!string.IsNullOrEmpty(value) ;
}
