using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fltrni.core.DTO
{
    public class Pagination<T>
        {
            public int PageIndex { get; set; }
            public int PageSize { get; set; }
            public int Count { get; set; }

            public IReadOnlyList<T> Data { get; set; }= new List<T>();
        }
    }

