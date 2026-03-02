class NullNodeConverter :
    WriteOnlyJsonConverter<NullExecutionNode>
{
    public override void Write(VerifyJsonWriter writer, NullExecutionNode value) =>
        writer.WriteNull();
}
