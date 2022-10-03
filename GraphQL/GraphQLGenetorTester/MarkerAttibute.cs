[AttributeUsage(AttributeTargets.All)]
public class MarkerAttibute : Attribute
{
    public MarkerAttibute(string operationName)
    {
        OperationName = operationName;
    }
    public string OperationName { get; }
    public string Description { get; set; } = string.Empty;
}
