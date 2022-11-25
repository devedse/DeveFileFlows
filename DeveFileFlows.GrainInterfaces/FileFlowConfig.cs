namespace DeveFileFlows.GrainInterfaces
{
    [Immutable, GenerateSerializer]
    public record class FileFlowConfig(
         string Name
         );
}
