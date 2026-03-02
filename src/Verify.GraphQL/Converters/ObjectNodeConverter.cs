class ObjectNodeConverter :
    WriteOnlyJsonConverter<ObjectExecutionNode>
{
    public override void Write(VerifyJsonWriter writer, ObjectExecutionNode value)
    {
        if (value.SubFields == null)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteStartObject();
        foreach (var child in value.SubFields)
        {
            writer.WriteMember(value, child, child.Name!);
        }

        writer.WriteEndObject();
    }
}
