using DeveFileFlows.GrainInterfaces;

namespace DeveFileFlows
{
    public class StepRunnerGrain : IStepRunnerGrain
    {
        private FileFlowStep _fileFlowStep;
        private IStepRunnerGrain _nextStepGrain;


        public Task SetStepConfig(FileFlowStep fileFlowStep)
        {
            _fileFlowStep = fileFlowStep;
            return Task.CompletedTask;
        }

        public Task SetNextStep(IStepRunnerGrain nextStepGrain)
        {
            _nextStepGrain = nextStepGrain;
            return Task.CompletedTask;
        }

        public async Task RunAsync(string filePath)
        {
            Console.WriteLine($"Processing Step: {_fileFlowStep.tool} {_fileFlowStep.arguments} on file {filePath} ...");
            await Task.Delay(5000);
            Console.WriteLine($"Done processing Step: {_fileFlowStep.tool}");
            if (_nextStepGrain != null)
            {
                await _nextStepGrain.RunAsync(filePath);
            }
        }

    }
}
