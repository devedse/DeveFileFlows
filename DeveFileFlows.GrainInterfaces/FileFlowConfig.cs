namespace DeveFileFlows.GrainInterfaces
{
    [Immutable, GenerateSerializer]
    public record class FileFlowConfig
    {
        public string Name { get; set; }
    };
}
