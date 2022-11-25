using DeveFileFlows.GrainInterfaces;

namespace DeveFileFlows
{
    public class StepRunnerGrain : IStepRunnerGrain
    {
        private IStepRunnerGrain _nextStepGrain;

        public Task SetNextStep(IStepRunnerGrain nextStepGrain)
        {
            _nextStepGrain = nextStepGrain;
            return Task.CompletedTask;
        }

        public async Task RunAsync(string filePath)
        {
            Console.WriteLine("Processing Step ...");
            await Task.Delay(5000);
            Console.WriteLine("Done processing Step");
            if (_nextStepGrain != null)
            {
                await _nextStepGrain.RunAsync(filePath);
            }
        }
    }
}
