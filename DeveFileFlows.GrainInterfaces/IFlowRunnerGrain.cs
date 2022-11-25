namespace DeveFileFlows.GrainInterfaces
{
    public interface IFlowRunnerGrain : IGrainWithIntegerKey
    {
        Task RunFlow(int flowId, string filePath);
    }
}
