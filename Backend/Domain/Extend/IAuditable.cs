using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extend
{
    public interface IAuditable
    {
        int CreatedBy { get; set; }
        DateTime Created { get; set; }
        int UpdatedBy { get; set; }
        DateTime Updated { get; set; }
    }
}
