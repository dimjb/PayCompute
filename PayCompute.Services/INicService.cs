using System;
using System.Collections.Generic;
using System.Text;

namespace PayCompute.Services
{
    public interface INicService
    {
        decimal NIContribution(decimal totalAmount);
    }
}
