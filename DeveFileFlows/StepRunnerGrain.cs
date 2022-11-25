using DeveFileFlows.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await Task.Delay(5000);
            if (_nextStepGrain != null)
            {
                await _nextStepGrain.RunAsync(filePath);
            }
        }
    }
}
