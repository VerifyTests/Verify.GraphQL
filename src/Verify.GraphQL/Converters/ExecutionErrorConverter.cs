class ExecutionErrorConverter :
    WriteOnlyJsonConverter<ExecutionError>
{
    public override void Write(VerifyJsonWriter writer, ExecutionError value)
    {
        writer.WriteStartObject();

        writer.WriteMember(value, value.Message, "Message");

        var locations = value.Locations;
        if (locations != null)
        {
            var locationsList = locations.ToList();
            if (locationsList.Count > 0)
            {
                writer.WritePropertyName("Locations");
                writer.WriteStartArray();
                foreach (var location in locationsList)
                {
                    writer.WriteStartObject();
                    writer.WriteMember(location, location.Line, "Line");
                    writer.WriteMember(location, location.Column, "Column");
                    writer.WriteEndObject();
                }

                writer.WriteEndArray();
            }
        }

        var path = value.Path;
        if (path != null)
        {
            var pathList = path.ToList();
            if (pathList.Count > 0)
            {
                writer.WriteMember(value, pathList, "Path");
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
