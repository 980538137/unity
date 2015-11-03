using Framework.OSAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.SocketMsgHandle
{
    public class CmdRegisterFactoryFacade : Framework.Singleton<CmdRegisterFactoryFacade>
    {
        // 命令创建工厂集合
        private Dictionary<UInt32, CmdRegisterFactoryBase> mFactorys = new Dictionary<UInt32, CmdRegisterFactoryBase>();

        private CmdRegisterFactoryFacade()
        {

        }

        public void RegisterCmdFactory(UInt32 cmdId, CmdRegisterFactoryBase factory)
        {
            if (null == factory)
            {
                throw new Exception("命令工厂为null");
            }

            mFactorys.Add(cmdId, factory);
        }

        /// <summary>
        ///  检查是否存在这样的命令
        /// </summary>
        /// <param name="cmdId">命令码</param>
        /// <returns>是否存在</returns>
        public bool CheckCmd(UInt32 cmdId)
        {
            return mFactorys.ContainsKey(cmdId);
        }

        /// <summary>
        /// 创建命令
        /// </summary>
        /// <param name="cmdId">命令码</param>
        /// <returns></returns>
        public CmdBase CreateCmd(UInt32 cmdId)
        {
            if(false == mFactorys.ContainsKey(cmdId))
            {
               // CSysLog.Instance.Write(SysLogLevel.ERR, String.Format("没有找到对应的命令[{0}]", cmdId));
                return null;
            }

            // 找到了对应的命令
            CmdBase cmd = mFactorys[cmdId].CreateCmd();

            return cmd;
        }
    }
}
