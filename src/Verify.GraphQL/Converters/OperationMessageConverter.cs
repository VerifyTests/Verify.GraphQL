class OperationMessageConverter :
    WriteOnlyJsonConverter<OperationMessage>
{
    public override void Write(VerifyJsonWriter writer, OperationMessage value)
    {
        writer.WriteStartObject();

        writer.WriteMember(value, value.Type, "Type");
        writer.WriteMember(value, value.Id, "Id");
        writer.WriteMember(value, value.Payload, "Payload");

        writer.WriteEndObject();
    }
}
