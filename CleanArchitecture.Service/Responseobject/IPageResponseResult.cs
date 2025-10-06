using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Service.Responseobject
{
    public interface IPageResponseResult<TEntity>
    {
        public int TotalRecord {  get; set; }

        public IList<TEntity> Entities { get; set; }
    }
}
