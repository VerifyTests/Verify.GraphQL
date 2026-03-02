class ResultConverter :
    WriteOnlyJsonConverter<ExecutionResult>
{
    public override void Write(VerifyJsonWriter writer, ExecutionResult value)
    {
        writer.WriteStartObject();

        var errors = value.Errors;
        if (errors is { Count: > 0 })
        {
            writer.WriteMember(value, errors, "Errors");
        }

        if (value.Executed)
        {
            var data = value.Data;
            if (data != null)
            {
                if (data is ExecutionNode node)
                {
                    data = node.ToValue();
                }

                writer.WriteMember(value, data, "Data");
            }
        }

        var extensions = value.Extensions;
        if (extensions is { Count: > 0 })
        {
            writer.WriteMember(value, extensions, "Extensions");
        }

        writer.WriteEndObject();
    }
}
