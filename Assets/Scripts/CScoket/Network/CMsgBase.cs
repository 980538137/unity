using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    public class CMsgBase
    {
        /// <summary>
        /// 主类型
        /// </summary>
        public Int32 MainType { get; set; }

        /// <summary>
        /// 子类型
        /// </summary>
        public Int32 SubType { get; set; }

        public CMsgBase(Int32 maintype, Int32 subtype)
        {
            MainType = maintype;
            SubType = subtype;
        }
    }
