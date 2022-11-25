namespace DeveFileFlows.GrainInterfaces
{
    [Immutable, GenerateSerializer]
    public record class FileFlowStep(
        string tool,
        string arguments
        );
}
