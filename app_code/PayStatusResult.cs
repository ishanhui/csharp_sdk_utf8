/// <summary>
/// 订单支付状态查询结果
/// </summary>
namespace cn.ishanhui
{
    public class PayStatusResult : OpenapiResponse
    {
        /// <summary>
        /// 商户订单号
        /// </summary>
        public string outTradeNo
        {
            get; set;
        }

        /// <summary>
        /// 支付流水号，该笔交易的支付平台的唯一流水号
        /// </summary>
        public string payId
        {
            get; set;
        }

        /// <summary>
        /// 渠道交易流水号
        /// </summary>
        public string chnlTradeNo
        {
            get;set;
        }

        /// <summary>
        /// 支付金额，单位分
        /// </summary>
        public int payMoney
        {
            get; set;
        }

        /// <summary>
        /// 支付时间，格式：yyyyMMddHHmmss
        /// </summary>
        public string payAt
        {
            get; set;
        }

        /// <summary>
        /// 支付状态,1：支付成功；0：未支付；-1：支付失败
        /// </summary>
        public int payStatus
        {
            get; set;
        }


    }
}
