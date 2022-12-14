using DeveFileFlows.GrainInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveFileFlows
{
    public class FlowRunnerGrain : IFlowRunnerGrain
    {
        private readonly IGrainFactory _grainFactory;

        public FlowRunnerGrain(IGrainFactory grainFactory)
        {
            _grainFactory = grainFactory;
        }

        public async Task RunFlow(int flowId, string filePath)
        {
            var golfGraan = _grainFactory.GetGrain<IFileFlowGrain>(flowId);

            var steps = await golfGraan.GetSteps();
            Console.WriteLine($"Run Flow: steps {steps.Count}");

            for (int i = 0; i < steps.Count; i++)
            {
                var step = steps[i];

                var stepRunner = _grainFactory.GetGrain<IStepRunnerGrain>($"{flowId}_{step.tool}");
                await stepRunner.SetStepConfig(step);

                if (i != steps.Count - 1)
                {
                    var nextStep = _grainFactory.GetGrain<IStepRunnerGrain>($"{flowId}_{steps[i + 1].tool}");
                    await stepRunner.SetNextStep(nextStep);
                }
            }

            var stepRunnerGogo = _grainFactory.GetGrain<IStepRunnerGrain>($"{flowId}_{steps[0].tool}");
            stepRunnerGogo.RunAsync(filePath).Ignore();
        }
    }
}
