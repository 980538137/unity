using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public enum CMsgMainType
    {
        NONE,

        /// <summary>
        /// 维护类型消息
        /// </summary>
        MAINTENANCE,
        
        /// <summary>
        /// 系统日志消息
        /// </summary>
        SYSLOG,

        /// <summary>
        /// 状态机定时器消息
        /// </summary>
        STATEMACHINETIMER,

        /// <summary>
        /// 用户自定义消息
        /// </summary>
        USER
    }

    public enum CMsgMaintenanceSubType
    {
        NONE,

        /// <summary>
        /// 关闭此线程
        /// </summary>
        SHUTDOWN,

        /// <summary>
        /// 状态机定时器超时
        /// </summary>
        TIMEOUT
    }
