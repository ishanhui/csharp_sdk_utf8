using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace cn.ishanhui.sdk
{
    /// <summary>
    /// 类名：Notify
    /// 功能：聚合支付通知处理类
    /// 详细：处理聚合支付接口通知返回
    /// 版本：1.0
    /// 修改日期：2015-11-11
    /// '说明：
    /// 以下代码只是为了方便商户测试而提供的样例代码，商户可以根据自己网站的需要，按照技术文档编写,并非一定要使用该代码。
    /// 该代码仅供学习和研究聚合支付接口使用，只是提供一个参考。
    /// 
    /// //////////////////////注意/////////////////////////////
    /// 调试通知返回时，可查看或改写log日志的写入TXT里的数据，来检查通知返回是否正常 
    /// </summary>
    public class Notify
    {
        #region 字段
        private string _merchantId = "";           //合作身份者ID
        private string _privateKey = "";           //商户的私钥
        private string _charsetMame = "";          //编码格式
        private string _signType = "";             //签名方式


        private string _serverUrl = "";            //网关地址

        private bool _debug = true;               //是否开启调试模式   



        #endregion


        /// <summary>
        /// 构造函数
        /// 从配置文件中初始化变量
        /// </summary>
        public Notify()
        {
            //初始化基础配置信息
            _merchantId = Config.MerchantId.Trim();
            _privateKey = Config.PrivateKey.Trim();
            _charsetMame = Config.CharsetName.Trim().ToLower();
            _signType = Config.SignType.Trim().ToUpper();
            _serverUrl = Config.ServerUrl.Trim();
        }

        public bool Debug
        {
            get { return _debug; }
            set { _debug = value; }
        }



        /// <summary>
        ///  验证消息ID合法性
        /// </summary>
        /// <param name="notifyId">通知验证ID</param>
        /// <returns>验证结果</returns>
        public bool VerifyNotifyId(string notifyId)
        {
            //获取返回时的签名验证结果

            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("merchantId", _merchantId);
            sParaTemp.Add("notifyId", notifyId);
            string responseTxt = OpenapiRequest.HttpGet(sParaTemp, "/pay/verify.do");
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            OpenapiResponse response = serializer.Deserialize<OpenapiResponse>(responseTxt);
            if (_debug)
            {
                Core.LogResult(responseTxt);
            }
            return response.isSuccess();
        }

        /// <summary>
        /// 获取待签名字符串（调试用）
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <returns>待签名字符串</returns>
        private string GetPreSignStr(SortedDictionary<string, string> inputPara)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();

            //过滤空值、sign与sign_type参数
            sPara = Core.FilterPara(inputPara);

            //获取待签名字符串
            string preSignStr = Core.CreateLinkString(sPara);

            return preSignStr;
        }

        /// <summary>
        /// 获取返回时的签名验证结果
        /// </summary>
        /// <param name="inputPara">通知返回参数数组</param>
        /// <param name="sign">对比的签名结果</param>
        /// <returns>签名验证结果</returns>
        public bool verifySign(SortedDictionary<string, string> inputPara, string sign)
        {
            Dictionary<string, string> sPara = new Dictionary<string, string>();

            //过滤空值、sign与sign_type参数
            sPara = Core.FilterPara(inputPara);

            //获取待签名字符串
            string preSignStr = Core.CreateLinkString(sPara);

            //获得签名验证结果
            bool isSgin = false;
            if (sign != null && sign != "")
            {
                switch (_signType)
                {
                    case "MD5":
                        isSgin = OpenapiMD5.Verify(preSignStr, sign, _privateKey, _charsetMame);
                        break;
                    default:
                        break;
                }
            }

            return isSgin;
        }

    }
}