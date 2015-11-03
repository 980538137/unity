using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.OSAbstract
{
    public enum SocketErrorRet
    {
        /// <summary>
        /// 成功
        /// </summary>
        SOCK_OK = 0,
        /// <summary>
        /// 失败
        /// </summary>
        SOCK_ERR = -1,
        /// <summary>
        /// 参数错误
        /// </summary>
        SOCK_PARA_ERR = -2,
        /// <summary>
        /// 套接字还没有创建
        /// </summary>
        SOCK_NOT_EXIST = -3,
        /// <summary>
        /// 没有实现
        /// </summary>
        SOCK_NOT_IMPLEMENT = -4,
        /// <summary>
        /// 创建套接字失败
        /// </summary>
        SOCK_CREATE_FAIL   = -5,
        /// <summary>
        /// 连接不存在
        /// </summary>
        SOCK_CONN_NOTEXIST = -6
    }
}
