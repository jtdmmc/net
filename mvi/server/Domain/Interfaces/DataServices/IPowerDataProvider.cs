using DotnetSoftwarePlatform.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Domain.Interfaces.DataServices
{
    public interface IPowerDataProvider
    {
        Task<IEnumerable<Power>> GetAllPowers();
    }
}
