using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveFileFlows.GrainInterfaces
{
    public interface IStepRunnerGrain : IGrainWithStringKey
    {
        Task SetStepConfig(FileFlowStep fileFlowStep);
        Task SetNextStep(IStepRunnerGrain nextStepGrain);
        Task RunAsync(string filePath);
    }
}
