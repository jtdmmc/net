using DotnetSoftwarePlatform.Domain.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.Domain.Services
{
    public sealed class PageAccountResult
    {
        public int TotalCount 
        { 
            get;
        }
        public int PageSize 
        { 
            get; 
        }
        public int CurrPage 
        { 
            get;
        }
        public IList<User> Data 
        { 
            get; 
        }

        public PageAccountResult(int totalCount, int pageSize, int currPage, IList<User> data)
        {
            TotalCount = totalCount;
            PageSize = pageSize;
            CurrPage = currPage;
            Data = data;
        }
    }
}
