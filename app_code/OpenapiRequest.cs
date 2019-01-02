using System.Text;
using System.IO;
using System.Net;
using System;
using System.Collections.Generic;

namespace cn.ishanhui.sdk
{

    public class OpenapiRequest
    {
        #region 字段
        //支付网关地址
        private static string _serverUrl = "";
        //商户的私钥
        private static string _privateKey = "";
        //编码格式
        private static string _charsetName = "";
        //签名方式
        private static string _signType = "";
        #endregion

        static OpenapiRequest()
        {
            _privateKey = Config.PrivateKey.Trim();
            _charsetName = Config.CharsetName.Trim().ToLower();
            _signType = Config.SignType.Trim().ToUpper();
            _serverUrl = Config.ServerUrl.Trim();
        }

        /// <summary>
        /// 生成请求时的签名
        /// </summary>
        /// <param name="sPara">需要传递给支付网关的参数</param>
        /// <returns>签名结果</returns>
        private static string BuildRequestMysign(Dictionary<string, string> sPara)
        {
            //把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            string prestr = Core.CreateLinkString(sPara);

            //把最终的字符串签名，获得签名结果
            string mysign = "";
            switch (_signType)
            {
                case "MD5":
                    mysign = OpenapiMD5.Sign(prestr, _privateKey, _charsetName);
                    break;
                default:
                    mysign = "";
                    break;
            }

            return mysign;
        }

        /// <summary>
        /// 生成要请求给支付网关的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <returns>要请求的参数数组</returns>
        private static Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            //待签名请求参数数组
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            //签名结果
            string mysign = "";

            //过滤签名参数数组
            sPara = Core.FilterPara(sParaTemp);

            //获得签名结果
            mysign = BuildRequestMysign(sPara);

            //签名结果与签名方式加入请求提交参数组中
            sPara.Add("sign", mysign);


            return sPara;
        }

        /// <summary>
        /// 生成要请求给支付网关的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <param name="code">字符编码</param>
        /// <returns>要请求的参数数组字符串</returns>
        private static string BuildRequestParaToString(SortedDictionary<string, string> sParaTemp, Encoding code)
        {
            //待签名请求参数数组
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            sPara = BuildRequestPara(sParaTemp);

            //把参数组中所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串，并对参数值做urlencode
            string strRequestData = Core.CreateLinkStringUrlencode(sPara, code);

            return strRequestData;
        }

        /// <summary>
        /// 建立请求，以表单HTML形式构造（默认）
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="uri">请求路径</param>
        /// <param name="strMethod">提交方式。两个值可选：post、get</param>
        /// <returns>提交表单HTML文本</returns>
        public static string FormSubmit(SortedDictionary<string, string> sParaTemp, String uri, string strMethod)
        {
            //待请求参数数组
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = BuildRequestPara(sParaTemp);

            StringBuilder sbHtml = new StringBuilder();

            sbHtml.Append("<form id='formPost' name='formPost' action='" + _serverUrl +  uri + "' method='" + strMethod.ToLower().Trim() + "'>");

            foreach (KeyValuePair<string, string> temp in dicPara)
            {
                sbHtml.Append("<input type='hidden' name='" + temp.Key + "' value='" + temp.Value + "'/>");
            }
            sbHtml.Append("</form>");
            sbHtml.Append("<script>document.forms['formPost'].submit();</script>");
            return sbHtml.ToString();
        }


        /// <summary>
        /// HTTP POST 方式 传输数据，并返回结果
        /// </summary>
        /// <param name="sParaTemp">请求参数数组</param>
        /// <param name="uri">请求路径</param>
        /// <returns>支付网关处理结果</returns>
        public static string HttpPost(SortedDictionary<string, string> sParaTemp, string uri)
        {
            Encoding code = Encoding.GetEncoding(_charsetName);

            //待请求参数数组字符串
            string strRequestData = BuildRequestParaToString(sParaTemp, code);

            //把数组转换成流中所需字节数组类型
            byte[] bytesRequestData = code.GetBytes(strRequestData);

            //构造请求地址
            string strUrl = _serverUrl + uri;

          

            //设置HttpWebRequest基本信息
            HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            myReq.Timeout = Config.Timeout;
            myReq.Method = "post";
            myReq.ContentType = "application/x-www-form-urlencoded";

            //填充POST数据
            myReq.ContentLength = bytesRequestData.Length;
            Stream requestStream = myReq.GetRequestStream();
            requestStream.Write(bytesRequestData, 0, bytesRequestData.Length);
            requestStream.Close();

            //发送POST数据请求服务器
            HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
            Stream myStream = HttpWResp.GetResponseStream();

            //获取服务器返回信息
            StreamReader reader = new StreamReader(myStream, code);
            StringBuilder responseData = new StringBuilder();
            String line;
            while ((line = reader.ReadLine()) != null)
            {
                responseData.Append(line);
            }

            //释放
            myStream.Close();

            return responseData.ToString();
        }



        /// <summary>
        /// http get 请求
        /// </summary>
        /// <param name="sParaTemp">请求数据</param>
        /// <param name="strUrl">请求路径</param>
        /// <returns>服务器ATN结果</returns>
        public static string HttpGet(SortedDictionary<string, string> sParaTemp, string uri)
        {
            Encoding code = Encoding.GetEncoding(_charsetName);
            //待请求参数数组字符串
            string strRequestData = BuildRequestParaToString(sParaTemp, code);

       
            //构造请求地址
            string strUrl = _serverUrl + uri + "?"+ strRequestData;


            HttpWebRequest myReq = (HttpWebRequest)HttpWebRequest.Create(strUrl);
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;	

            myReq.Timeout = Config.Timeout;
            HttpWebResponse HttpWResp = (HttpWebResponse)myReq.GetResponse();
            Stream myStream = HttpWResp.GetResponseStream();
            StreamReader sr = new StreamReader(myStream, code);
            StringBuilder strBuilder = new StringBuilder();
            while (-1 != sr.Peek())
            {
                strBuilder.Append(sr.ReadLine());
            }

            return strBuilder.ToString();
        }
    }
}