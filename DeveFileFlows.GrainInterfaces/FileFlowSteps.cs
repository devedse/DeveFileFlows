namespace DeveFileFlows.GrainInterfaces
{
    [Immutable, GenerateSerializer]
    public record FileFlowSteps
    {
        [Id(0)]
        public IList<FileFlowStep> Steps { get; set; } = new List<FileFlowStep>();
    }
}
