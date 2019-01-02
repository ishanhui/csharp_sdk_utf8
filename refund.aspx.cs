using cn.ishanhui;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
/**
 * 功能：1.10 退款发起
 * 说明：以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己的实际情况，按照技术文档重新编写。
 */
public partial class _refund : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        sParaTemp.Add("merchantId", Config.MerchantId);

        sParaTemp.Add("outTradeNo", "fund123456789");//商户平台的退款流水号(请确保唯一性)
        sParaTemp.Add("payTradeNo", "test145878314074");//商户原交易订单号

        sParaTemp.Add("refundMoney", "10");//退款金额，单位分,无小数点
     
        try
        {
            string result = OpenapiRequest.HttpPost(sParaTemp, "/pay/refund.do");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            OpenapiResponse response = serializer.Deserialize<QrCodeResult>(result);

            if (response.isSuccess())
            {
                Response.Write("发起退款成功");

            }
            else
            {
                Response.Write("发起退款失败:" + response.retMsg);
            }
        }
        catch (Exception exp)
        {
            Response.Write("HTTP POST ERROR:" + exp.Message);
        }


    }



}
