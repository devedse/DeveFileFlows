using DeveFileFlows.Common.Grains;
using DeveFileFlows.Common.Pocos;
using Orleans.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveFileFlows.Grains
{
    public class FileFlowGrain : IFileFlowGrain
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
