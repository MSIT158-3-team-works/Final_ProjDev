using System.Net.Http.Headers;
using System.Text;
using System.Web;
using projRESTfulApiFitConnect.LinePay.Dtos;
using projRESTfulApiFitConnect.LinePay.Providers;

namespace projRESTfulApiFitConnect.LinePay.Domain
{
    public class LinePayService
    {
        private readonly string channelId;
        private readonly string channelSecretKey;

        private readonly string linePayBaseApiUrl = "https://sandbox-api-pay.line.me";

        private static HttpClient client;
        private readonly JsonProvider _jsonProvider;
        public LinePayService(IConfiguration configuration)
        {
            client = new HttpClient();
            _jsonProvider = new JsonProvider();
            channelId = configuration["LinePay:ChannelId"];
            channelSecretKey = configuration["LinePay:ChannelSecretKey"];
        }

        // 送出建立交易請求至 Line Pay Server
        public async Task<PaymentResponseDto> SendPaymentRequest(PaymentRequestDto dto)
        {
            var json = _jsonProvider.Serialize(dto);
            // 產生 GUID Nonce
            var nonce = Guid.NewGuid().ToString();
            // 要放入 signature 中的 requestUrl
            var requestUrl = "/v3/payments/request";

            //使用 channelSecretKey & requestUrl & jsonBody & nonce 做簽章
            var signature = SignatureProvider.HMACSHA256(channelSecretKey, channelSecretKey + requestUrl + json + nonce);

            var request = new HttpRequestMessage(HttpMethod.Post, linePayBaseApiUrl + requestUrl)
            {
                Content = new StringContent(json, Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json"))
            };
            // 帶入 Headers
            client.DefaultRequestHeaders.Add("X-LINE-ChannelId", channelId);
            client.DefaultRequestHeaders.Add("X-LINE-Authorization-Nonce", nonce);
            client.DefaultRequestHeaders.Add("X-LINE-Authorization", signature);

            var response = await client.SendAsync(request);
            var linePayResponse = _jsonProvider.Deserialize<PaymentResponseDto>(await response.Content.ReadAsStringAsync());

            Console.WriteLine(nonce);
            Console.WriteLine(signature);

            return linePayResponse;
        }
        // 取得 transactionId 後進行確認交易
        public async Task<PaymentConfirmResponseDto> ConfirmPayment(string transactionId, string orderId, PaymentConfirmDto dto) //加上 OrderId 去找資料
        {
            var json = _jsonProvider.Serialize(dto);

            var nonce = Guid.NewGuid().ToString();
            var requestUrl = string.Format("/v3/payments/{0}/confirm", transactionId);
            var signature = SignatureProvider.HMACSHA256(channelSecretKey, channelSecretKey + requestUrl + json + nonce);

            var request = new HttpRequestMessage(HttpMethod.Post, String.Format(linePayBaseApiUrl + requestUrl, transactionId))
            {
                Content = new StringContent(json, Encoding.UTF8, MediaTypeHeaderValue.Parse("application/json"))
            };

            client.DefaultRequestHeaders.Add("X-LINE-ChannelId", channelId);
            client.DefaultRequestHeaders.Add("X-LINE-Authorization-Nonce", nonce);
            client.DefaultRequestHeaders.Add("X-LINE-Authorization", signature);

            var response = await client.SendAsync(request);
            var responseDto = _jsonProvider.Deserialize<PaymentConfirmResponseDto>(await response.Content.ReadAsStringAsync());
            return responseDto;
        }
        public void TransactionCancel(string transactionId)
        {
            //使用者取消交易則會到這裏。
            Console.WriteLine($"訂單 {transactionId} 已取消");
        }
    }
}
