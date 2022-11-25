using DeveFileFlows.GrainInterfaces;
using Orleans.Runtime;

namespace DeveFileFlows
{
    public class FileFlowGrain : Grain, IFileFlowGrain
    {

        private readonly IPersistentState<FileFlowConfig> _fileFlowConfigState;
        private readonly IPersistentState<IList<FileFlowStep>> _stepsState;

        public FileFlowGrain(
            [PersistentState("fileFlowConfig", "ultraStore")] IPersistentState<FileFlowConfig> fileFlowConfigState)
            //[PersistentState("steps", "ultraStore")] IPersistentState<IList<FileFlowStep>> stepsState
        {
            _fileFlowConfigState = fileFlowConfigState;
            //_stepsState = stepsState;
        }



        public Task<FileFlowConfig> GetFileFlowConfig()
        {
            return Task.FromResult(_fileFlowConfigState.State);
        }

        public Task<IList<FileFlowStep>> GetSteps()
        {
            return Task.FromResult(_stepsState.State);
        }

        public Task SetFileFlowConfig(FileFlowConfig config)
        {
            _fileFlowConfigState.State = config;
            return _fileFlowConfigState.WriteStateAsync();
        }

        public Task SetSteps(IList<FileFlowStep> steps)
        {
            _stepsState.State = steps;
            return _stepsState.WriteStateAsync();
        }
    }
}
