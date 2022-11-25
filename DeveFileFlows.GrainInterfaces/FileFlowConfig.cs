namespace DeveFileFlows.GrainInterfaces
{
    [Immutable, GenerateSerializer]
    public record FileFlowConfig
    {
        [Id(0)]
        public string Name { get; set; } = "";

        [Id(1)]
        public string Description { get; set; }
    };
}
