using cn.ishanhui.sdk;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

/**
 * 功能：2.1 报关推送
 * 说明：以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己的实际情况，按照技术文档重新编写。
 */
public partial class customReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        sParaTemp.Add("merchantId", Config.MerchantId);

        sParaTemp.Add("outTradeNo", "test145878314074");//请求流水号，唯一标识该笔请求
        sParaTemp.Add("payTradeNo", "");//支付单对应的请求流水号(支付接口对应的outTradeNo)
        sParaTemp.Add("oriMchOrder", "");//商户平台订单号
        sParaTemp.Add("goodsDesc", "测试订单");//订单描述
        sParaTemp.Add("goodsNum", "1");//商品数量
        sParaTemp.Add("payMoney", "100");//支付金额，单位分（100表示1元）
        sParaTemp.Add("goodsMoney", "100");//支付货款，单位分（100表示1元）
        sParaTemp.Add("freight", "0");//支付运费，单位分（100表示1元）
        sParaTemp.Add("tax", "0");//支付税款，单位分（100表示1元）
        sParaTemp.Add("customType", "");//海关通道，参考《海关通道列表》
        sParaTemp.Add("customCode", "");//海关编码，参考《海关代码表》
        sParaTemp.Add("ciqCode", "");//国检编码，参考《国检代码表》（需要上报国检时填写）
        sParaTemp.Add("note", "");//备注信息
        sParaTemp.Add("ip", "");//交易终端的IP地址


        try
        {
            string result = OpenapiRequest.HttpPost(sParaTemp, "/custom/report.do");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            OpenapiResponse response = serializer.Deserialize<QrCodeResult>(result);

            if (response.isSuccess())
            {
                Response.Write("报关信息提交成功");

            }
            else
            {
                Response.Write("报关信息提交失败:" + response.retMsg);
            }
        }
        catch (Exception exp)
        {
            Response.Write("HTTP POST ERROR:" + exp.Message);
        }
    }
}