using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// 二维码结果
/// </summary>
namespace cn.ishanhui.sdk
{
    public class QrCodeResult : OpenapiResponse
    {
       
        public string qrCodeUrl{ set; get; }
        
    }
}
