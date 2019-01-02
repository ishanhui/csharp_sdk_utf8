using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

using cn.ishanhui;
/**
 * 功能：1.5 网银支付 接口
 * 说明：以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己的实际情况，按照技术文档重新编写。
 */
public partial class _ebank : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        sParaTemp.Add("merchantId", Config.MerchantId);


        sParaTemp.Add("outTradeNo", "test145878314104");//商户平台的订单号，确保唯一性
      
        sParaTemp.Add("payMoney", "10");//支付金额，单位分,无小数点
        sParaTemp.Add("goodsDesc", "测试订单");//订单描述

        sParaTemp.Add("cardType", "1");//银行卡类型: 1-借记卡，2-贷记卡
        sParaTemp.Add("bankNo", "CCB");//银行代号，参见银行代号列表
        sParaTemp.Add("userType", "1");//用户类型：1-个人，2-企业

        sParaTemp.Add("buyerName", "张三");//买家姓名
        sParaTemp.Add("buyerCertNo", "510901197502176549");//买家身份证号
        sParaTemp.Add("buyerPhone", "13122334455");//买家手机号

        sParaTemp.Add("ip", Core.GetIPAddress());//交易终端的IP地址

        sParaTemp.Add("notifyUrl", Config.PayNotifyUrl.Trim());//支付结果异步通知地址
        sParaTemp.Add("returnUrl", Config.PayReturnUrl.Trim());//支付结果同步通知地址

        Response.Write(OpenapiRequest.FormSubmit(sParaTemp, "/pay/ebank.action", "post"));
    }



}
