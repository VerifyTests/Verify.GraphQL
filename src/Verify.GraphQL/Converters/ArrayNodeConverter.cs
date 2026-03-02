class ArrayNodeConverter :
    WriteOnlyJsonConverter<ArrayExecutionNode>
{
    public override void Write(VerifyJsonWriter writer, ArrayExecutionNode value)
    {
        var items = value.Items;
        if (items == null)
        {
            var result = value.SerializedResult;
            if (result == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.Serialize(result);
            }

            return;
        }

        writer.WriteStartArray();
        foreach (var child in items)
        {
            writer.Serialize(child);
        }

        writer.WriteEndArray();
    }
}
