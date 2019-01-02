using cn.ishanhui;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
/**
 * 功能：1.1    扫码支付 接口
 * 说明：以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己的实际情况，按照技术文档重新编写。
 */
public partial class _qrCode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        sParaTemp.Add("merchantId", Config.MerchantId);


        sParaTemp.Add("outTradeNo", "test20170101001");//商户平台的订单号，确保唯一性
        sParaTemp.Add("payType", "10");//10-获取微信二维码；20-获取支付宝二维码。

        sParaTemp.Add("payMoney", "10");//支付金额，单位分,无小数点
        sParaTemp.Add("goodsDesc", "测试订单");//订单描述

        sParaTemp.Add("ip", Core.GetIPAddress());//交易终端的IP地址

        sParaTemp.Add("notifyUrl", Config.PayNotifyUrl.Trim());//支付结果异步通知地址

        sParaTemp.Add("buyerName", "张三");//买家姓名
        sParaTemp.Add("buyerCertNo", "510901197502176549");//买家身份证号
        sParaTemp.Add("buyerPhone", "13122334455");//买家手机号

        try
        {
            string result = OpenapiRequest.HttpPost(sParaTemp, "/pay/qr.do");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            QrCodeResult qrCodeResult = serializer.Deserialize<QrCodeResult>(result);

            if (qrCodeResult.isSuccess())
            {
                // 请商户自行处理二维码渲染
                // javascript生成二维码图片 可参考 https://github.com/davidshimjs/qrcodejs
                Response.Write("获取二维码成功:" + qrCodeResult.qrCodeUrl);
            }
            else
            {
                Response.Write("获取二维码失败:" + qrCodeResult.retMsg);
            }
        }
        catch (Exception exp)
        {
            Response.Write("HTTP POST ERROR:" + exp.Message);
        }


    }



}
