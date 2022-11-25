namespace DeveFileFlows.GrainInterfaces
{
    public interface IFileFlowGrain : IGrainWithIntegerKey
    {
        Task<IList<FileFlowStep>> GetSteps();
        Task SetSteps(IList<FileFlowStep> steps);

        Task<FileFlowConfig> GetFileFlowConfig();
        Task SetFileFlowConfig(FileFlowConfig config);
    }
}
