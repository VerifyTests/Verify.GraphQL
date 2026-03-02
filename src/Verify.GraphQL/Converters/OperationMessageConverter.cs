class OperationMessageConverter :
    WriteOnlyJsonConverter<OperationMessage>
{
    public override void Write(VerifyJsonWriter writer, OperationMessage value)
    {
        writer.WriteStartObject();

        if (value.Type != null)
        {
            writer.WriteMember(value, value.Type, "Type");
        }

        if (value.Id != null)
        {
            writer.WriteMember(value, value.Id, "Id");
        }

        if (value.Payload != null)
        {
            writer.WriteMember(value, value.Payload, "Payload");
        }

        writer.WriteEndObject();
    }
}
