using DotnetSoftwarePlatform.Domain.Interfaces.DataServices;
using DotnetSoftwarePlatform.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.DAL.EF.Services
{
    public class PowerDataProvider : IPowerDataProvider
    {
        public async Task<IEnumerable<Power>> GetAllPowers()
        {
            using (var db = new DotnetSoftwarePlatformContext())
            {
                return await db.Powers.ToListAsync();
            }
        }
    }
}
