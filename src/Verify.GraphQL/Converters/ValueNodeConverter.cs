class ValueNodeConverter :
    WriteOnlyJsonConverter<ValueExecutionNode>
{
    public override void Write(VerifyJsonWriter writer, ValueExecutionNode value)
    {
        var nodeValue = value.ToValue();
        if (nodeValue == null)
        {
            writer.WriteNull();
        }
        else
        {
            writer.Serialize(nodeValue);
        }
    }
}
