using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeveFileFlows.GrainInterfaces
{
    [Immutable, GenerateSerializer]
    public record class FileFlowConfig(
         string name
         );
}
