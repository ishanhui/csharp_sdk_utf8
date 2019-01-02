using cn.ishanhui;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

/// <summary>
/// 功能：页面跳转同步通知页面
/// 
/// ///////////////////////页面功能说明///////////////////////
/// 该页面可在本机电脑测试
/// 可放入HTML等美化页面的代码、商户业务逻辑程序代码
/// 该页面可以使用ASP.NET开发工具调试，也可以使用写文本函数LogResult进行调试
/// </summary>
public partial class _return_url : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SortedDictionary<string, string> sPara = GetRequestPost();
    
        try
        {
            if (sPara.Count == 0)
            {
                throw new Exception("no data");
            }

            Notify notify = new Notify();
            notify.Debug = true;//记录错误日志

            //校验商户号是否匹配
            if (!Request.Form["merchantId"].Equals(Config.MerchantId))
            {
                throw new Exception("merchantId error");
            }
            //校验签名是否匹配
            if (!notify.verifySign(sPara, Request.Form["sign"]))
            {
                throw new Exception("sign error");
            }
    
            //订单支付成功
            if (Request.Form["payStatus"].Equals("1"))
            {

                //判断该笔订单是否在商户网站中已经做过处理
                //如果没有做过处理，根据订单号（outTradeNo）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                //请务必判断请求时的payMoney与通知时获取的payMoney 为一致的
                //如果有做过处理，不执行商户的业务程序

            }

            Response.Write("success");

        }
        catch (Exception exp)
        {
            Response.Write("error:"+exp.Message);
        }
    }

    /// <summary>
    /// 获取网关POST过来通知消息，并以“参数名=参数值”的形式组成数组
    /// </summary>
    /// <returns>request回来的信息组成的数组</returns>
    public SortedDictionary<string, string> GetRequestPost()
    {
        int i = 0;
        SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
        NameValueCollection coll;
        //Load Form variables into NameValueCollection variable.
        coll = Request.Form;

        // Get names of all forms into a string array.
        String[] requestItem = coll.AllKeys;

        for (i = 0; i < requestItem.Length; i++)
        {
            sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
        }

        return sArray;
    }
}
