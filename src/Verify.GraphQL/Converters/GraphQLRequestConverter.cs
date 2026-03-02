using GraphQL.Transport;

class GraphQLRequestConverter :
    WriteOnlyJsonConverter<GraphQLRequest>
{
    public override void Write(VerifyJsonWriter writer, GraphQLRequest value)
    {
        writer.WriteStartObject();

        if (value.Query != null)
        {
            writer.WriteMember(value, value.Query, "Query");
        }

        if (value.OperationName != null)
        {
            writer.WriteMember(value, value.OperationName, "OperationName");
        }

        if (value.Variables != null)
        {
            writer.WriteMember(value, value.Variables, "Variables");
        }

        if (value.Extensions != null)
        {
            writer.WriteMember(value, value.Extensions, "Extensions");
        }

        if (value.DocumentId != null)
        {
            writer.WriteMember(value, value.DocumentId, "DocumentId");
        }

        writer.WriteEndObject();
    }
}
