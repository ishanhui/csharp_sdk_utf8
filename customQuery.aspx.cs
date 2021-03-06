﻿using cn.ishanhui.sdk;
using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
/**
 * 功能：2.2 报关查询
 * 说明：以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己的实际情况，按照技术文档重新编写。
 */
public partial class customQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
        sParaTemp.Add("merchantId", Config.MerchantId);

        sParaTemp.Add("outTradeNo", "test145878314075");//请求流水号

        try
        {
            string result = OpenapiRequest.HttpGet(sParaTemp, "/custom/query.do");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            PayStatusResult response = serializer.Deserialize<PayStatusResult>(result);

            if (response.isSuccess())
            {
                Response.Write("查询成功:" + result);

            }
            else
            {
                Response.Write("查询失败:" + response.retMsg);
            }
        }
        catch (Exception exp)
        {
            Response.Write("HTTP GET ERROR:" + exp.Message);
        }
    }
}