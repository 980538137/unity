using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.SocketMsgHandle
{
    /// <summary>
    /// 通过此类的各个实例来创建对象
    /// </summary>
    public class CmdRegisterFactory<T> : CmdRegisterFactoryBase where T : CmdBase, new()
    {
        /// <summary>
        /// 此命令工厂要处理具有命令
        /// </summary>
        public UInt32 CmdID { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cmdId">命令码</param>
        public CmdRegisterFactory(UInt32 cmdId)
        {
            CmdRegisterFactoryFacade.Instance.RegisterCmdFactory(cmdId, this);
        }

        /// <summary>
        /// 创建命令实例
        /// </summary>
        /// <returns>实例化出来的命令</returns>
        public override CmdBase CreateCmd()
        {
            CmdBase cmd = new T();

		    return cmd;
        }
    }
}
