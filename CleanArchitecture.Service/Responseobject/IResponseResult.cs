using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Service.Responseobject
{
    public interface IResponseResult<TEntity>:IResponse
    {
        public TEntity Entity { get; set; }
    }
}
