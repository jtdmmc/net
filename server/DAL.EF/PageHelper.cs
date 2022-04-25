using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotnetSoftwarePlatform.DAL.EF
{
    public static class PageHelper
    {
        /// <summary>
        /// 分页
        /// </summary>
        public static Task<PagedResult<T>> GetPaged<T>(this IQueryable<T> query,
                                                 int page, int pageSize) where T : class
        {
            return Task.Run(() => {
                var result = new PagedResult<T>();
                result.CurrentPage = page;
                result.PageSize = pageSize;
                result.RowCount = query.Count();


                var pageCount = (double)result.RowCount / pageSize;
                result.PageCount = (int)Math.Ceiling(pageCount);

                var skip = (page - 1) * pageSize;
                if(skip < 0)
                {
                    skip = 0;
                }
                result.Results = query.Skip(skip).Take(pageSize).ToList();

                return result;
            });
            
        }
    }
}
