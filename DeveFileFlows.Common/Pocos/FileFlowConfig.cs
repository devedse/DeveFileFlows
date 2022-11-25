using Orleans;

namespace DeveFileFlows.Common.Pocos
{
    [Immutable, GenerateSerializer]
    public record class FileFlowConfig(
        string name
        );
}
