using DeveFileFlows.Common.Pocos;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveFileFlows.Common.Grains
{
    public interface IFileFlowGrain : IGrainWithIntegerKey
    {
        Task<IList<FileFlowStep>> GetSteps();
        Task SetSteps(IList<FileFlowStep> steps);

        Task<FileFlowConfig> GetFileFlowConfig();
        Task SetFileFlowConfig(FileFlowConfig config);
    }
}
