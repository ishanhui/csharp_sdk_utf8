
namespace cn.ishanhui
{

    public class Config
    {
        //↓↓↓↓↓↓↓↓↓↓请在这里配置您的基本信息↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
        #region 字段
        private static string merchantId = "11111111";//商户号
        private static string privateKey = "200A710CEABD4518AF9BC3A7A183B85D";//分配的密钥
        private static string charsetName = "utf-8"; //字符编码格式 目前仅支持utf-8
        private static string signType = "MD5";//签名方式，目前仅支持MD5
        private static string serverUrl = "https://test.api.ishanhui.cn";//网关地址
        private static string payReturnUrl = "http://localhost:55016/return_url.aspx";//支付结果同步通知地址，不要带get参数
        private static string payNotifyUrl = "http://localhost:55016/notify_url.aspx";//支付结果异步通知地址，不要带get参数
        private static int timeout = 10000;//设置与网关通信超时时间,单位毫秒，默认10秒
        #endregion



        #region 属性
        /// <summary>
        /// 获取或设置商户号
        /// </summary>
        public static string MerchantId
        {
            get { return merchantId; }
            set { merchantId = value; }
        }



        /// <summary>
        /// 获取或设密钥
        /// </summary>
        public static string PrivateKey
        {
            get { return privateKey; }
            set { privateKey = value; }
        }

        /// <summary>
        /// 获取字符编码格式
        /// </summary>
        public static string CharsetName
        {
            get { return charsetName; }
        }

        /// <summary>
        /// 获取签名方式
        /// </summary>
        public static string SignType
        {
            get { return signType; }
        }

        /// <summary>
        /// 获取网关地址
        /// </summary>
        public static string ServerUrl
        {
            get { return serverUrl; }
            set { serverUrl = value; }
        }

        /// <summary>
        /// 获取或设支付结果同步通知地址
        /// </summary>
        public static string PayReturnUrl {
            get { return payReturnUrl; }
            set { payReturnUrl = value; }
        }


        /// <summary>
        /// 获取或设支付结果异步通知地址
        /// </summary>
        public static string PayNotifyUrl
        {
            get { return payNotifyUrl; }
            set { payNotifyUrl = value; }
        }

        /// <summary>
        /// 获取或设HTTP链接超时时间
        /// </summary>
        public static int Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        #endregion
    }
}