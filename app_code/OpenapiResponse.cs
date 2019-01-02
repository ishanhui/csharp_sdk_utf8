using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 网关响应数据
/// </summary>
namespace cn.ishanhui
{
    public class OpenapiResponse
    {
        public string retCode { set; get; }
        public string retMsg { set; get; }
     
        public bool isSuccess()
        {
            return retCode.Equals("00");
        }
    }
}

