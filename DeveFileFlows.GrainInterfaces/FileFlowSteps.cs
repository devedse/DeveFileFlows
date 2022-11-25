namespace DeveFileFlows.GrainInterfaces
{
    [Immutable, GenerateSerializer]
    public record FileFlowSteps
    {
        public IList<FileFlowStep> Steps { get; set; } = new List<FileFlowStep>();
    }
}
