using DeveFileFlows.Common.Grains;
using DeveFileFlows.Common.Pocos;

namespace DeveFileFlows.Grains
{
    public class FileFlowGrain : Grain, IFileFlowGrain
    {
        private FileFlowConfig _fileFlowConfig = new("");
        private IList<FileFlowStep> _steps = new List<FileFlowStep>();

        public Task<FileFlowConfig> GetFileFlowConfig()
        {
            return Task.FromResult(_fileFlowConfig);
        }

        public Task<IList<FileFlowStep>> GetSteps()
        {
            return Task.FromResult(_steps);
        }

        public Task SetFileFlowConfig(FileFlowConfig config)
        {
            _fileFlowConfig = config;
            return Task.CompletedTask;
        }

        public Task SetSteps(IList<FileFlowStep> steps)
        {
            _steps = steps;
            return Task.CompletedTask;
        }
    }
}
