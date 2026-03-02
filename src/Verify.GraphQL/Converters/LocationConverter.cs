using GraphQLParser;

class LocationConverter :
    WriteOnlyJsonConverter<Location>
{
    public override void Write(VerifyJsonWriter writer, Location value)
    {
        writer.WriteStartObject();
        writer.WriteMember(value, value.Line, "Line");
        writer.WriteMember(value, value.Column, "Column");
        writer.WriteEndObject();
    }
}
