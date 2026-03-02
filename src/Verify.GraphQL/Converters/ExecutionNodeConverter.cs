using GraphQL.Execution;

class ExecutionNodeConverter :
    WriteOnlyJsonConverter<ExecutionNode>
{
    public override void Write(VerifyJsonWriter writer, ExecutionNode value)
    {
        switch (value)
        {
            case NullExecutionNode:
                writer.WriteNull();
                break;
            case ValueExecutionNode valueNode:
                var nodeValue = valueNode.ToValue();
                if (nodeValue == null)
                {
                    writer.WriteNull();
                }
                else
                {
                    writer.Serialize(nodeValue);
                }

                break;
            case ObjectExecutionNode objectNode:
                WriteObject(writer, objectNode);
                break;
            case ArrayExecutionNode arrayNode:
                WriteArray(writer, arrayNode);
                break;
            default:
                var fallback = value.ToValue();
                if (fallback == null)
                {
                    writer.WriteNull();
                }
                else
                {
                    writer.Serialize(fallback);
                }

                break;
        }
    }

    static void WriteObject(VerifyJsonWriter writer, ObjectExecutionNode node)
    {
        if (node.SubFields == null)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteStartObject();
        foreach (var child in node.SubFields)
        {
            writer.WriteMember(node, child, child.Name!);
        }

        writer.WriteEndObject();
    }

    static void WriteArray(VerifyJsonWriter writer, ArrayExecutionNode node)
    {
        var items = node.Items;
        if (items == null)
        {
            var result = node.SerializedResult;
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
