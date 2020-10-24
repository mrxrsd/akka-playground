using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ErrorHandling
{
    public class ExternalService
    {

        public Task<string> GetGoodData()
        {
            return Task.FromResult("data");
        }

        public Task<string> GetErrorData()
        {
            throw new NullReferenceException();
        }
    }
}
