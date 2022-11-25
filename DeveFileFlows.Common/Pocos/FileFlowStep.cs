using Orleans;

namespace DeveFileFlows.Common.Pocos
{
    [Immutable, GenerateSerializer]
    public record class FileFlowStep(
        string tool,
        string arguments
        );
}
