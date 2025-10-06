using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Service.Responseobject
{
    public class PageResponseResult<TEntity>:IPageResponseResult<TEntity>
    {

        public PageResponseResult():this(new List<TEntity>(),0)
        {
            
        }
        public PageResponseResult(IList<TEntity> entities):this(entities,entities.Count)
        {
            
        }

        

        public PageResponseResult(IList<TEntity> entities,int total)
        {
            TotalRecord = total;
            Entities = entities;
        }
        public int TotalRecord { get; set; }

        public IList<TEntity> Entities { get; set; }

    }
}
