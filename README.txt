
───────
 代码文件结构
───────

csharp_sdk_utf8
  │
  ├app_code ┈┈┈┈┈┈┈┈┈┈类文件夹
  │  │
  │  ├MD5.cs┈┈┈┈┈┈┈┈┈┈┈┈MD5类库
  │  │
  │  ├OpenapiConfig.cs┈┈┈┈┈┈┈配置文件
  │  │
  │  ├OpenapiCore.cs┈┈┈┈┈┈┈┈公用函数类
  │  │
  │  ├OpenapiNotify.cs┈┈┈┈┈┈┈支付通知处理类
  │  │
  │  ├OpenapiRequest.cs┈┈┈┈┈┈ 接口请求处理类
  │  │
  │  ├OpenapiResponse.cs┈┈┈┈┈┈接口响应消息参数类
  │  │
  │  ├PayStatusResult.cs┈┈┈┈┈┈交易查询结果参数类
  │  │
  │  └QrCodeResult.cs┈┈┈┈┈┈┈ 二维码结果参数类
  │
  ├log┈┈┈┈┈┈┈┈┈┈┈┈┈┈┈ 日志文件夹
  │
  ├ebank.aspx ┈┈┈┈┈┈┈┈┈┈┈ 调用网银支付
  ├ebank.aspx.cs┈┈┈┈┈┈┈┈┈┈ 调用网银支付
  │
  ├qrCode.aspx ┈┈┈┈┈┈┈┈┈┈┈调用二维码主扫支付
  ├qrCode.aspx.cs┈┈┈┈┈┈┈┈┈┈调用二维码主扫支付
  │
  ├refund.aspx ┈┈┈┈┈┈┈┈┈┈┈调用退款接口
  ├refund.aspx.cs┈┈┈┈┈┈┈┈┈┈调用退款接口
  │
  ├query.aspx ┈┈┈┈┈┈┈┈┈┈┈ 调用交易结果查询接口
  ├query.aspx.cs┈┈┈┈┈┈┈┈┈┈ 调用交易结果查询接口
  │
  ├notify_url.aspx┈┈┈┈┈┈┈┈┈ 支付结果异步通知页面文件
  ├notify_url.aspx.cs ┈┈┈┈┈┈┈ 支付结果异步通知页面文件
  │
  ├return_url.aspx┈┈┈┈┈┈┈┈┈ 页面跳转同步通知文件
  ├return_url.aspx.cs ┈┈┈┈┈┈┈ 页面跳转同步通知文件
  │
  ├customReport.aspx ┈┈┈┈┈┈┈┈报关发起接口
  ├customReport.aspx.cs┈┈┈┈┈┈┈报关发起接口
  │  
  ├customQuery.aspx ┈┈┈┈┈┈┈┈ 报关查询接口
  ├customQuery.aspx.cs┈┈┈┈┈┈┈ 报关查询接口
  │
  ├Web.Config ┈┈┈┈┈┈┈┈┈┈┈ 配置文件（集成时删除）
  │
  └README.txt ┈┈┈┈┈┈┈┈┈┈┈ 使用说明文本

※注意※
需要配置的文件是：
OpenapiConfig.cs

