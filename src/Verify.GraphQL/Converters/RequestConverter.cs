class RequestConverter :
    WriteOnlyJsonConverter<GraphQLRequest>
{
    public override void Write(VerifyJsonWriter writer, GraphQLRequest value)
    {
        writer.WriteStartObject();

        writer.WriteMember(value, value.Query, "Query");
        writer.WriteMember(value, value.OperationName, "OperationName");
        writer.WriteMember(value, value.Variables, "Variables");
        writer.WriteMember(value, value.Extensions, "Extensions");
        writer.WriteMember(value, value.DocumentId, "DocumentId");

        writer.WriteEndObject();
    }
}
