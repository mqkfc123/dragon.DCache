using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DCache.RegisterServices
{
    public class RegisterService : IRegisterService
    {
        /// <summary>
        /// 开始流程
        /// </summary>
        /// <returns></returns>
        public static RegisterService Start()
        {
            var register = new RegisterService();

            return register;
        }
    }
}
