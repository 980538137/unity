using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.SocketMsgHandle
{
    public abstract class CmdRegisterFactoryBase
    {
        /// <summary>
        /// 创建命令实例
        /// </summary>
        /// <returns>实例化出来的命令</returns>
        public abstract CmdBase CreateCmd();
    }
}
