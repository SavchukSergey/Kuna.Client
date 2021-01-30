using Newtonsoft.Json;

namespace Kuna.Client {
    public class UserInfo {

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("kunaid")]
        public string KunaId { get; set; }

        [JsonProperty("two_factor")]
        public bool TwoFactor { get; set; }

        [JsonProperty("withdraw_confirmation")]
        public bool WithdrawConfirmation { get; set; }

        [JsonProperty("send_order_notice")]
        public bool SendOrderNotice { get; set; }

        [JsonProperty("send_withdraw_notice")]
        public bool SendWithdrawNotice { get; set; }

        [JsonProperty("send_signin_notice")]
        public bool SendSigninNotice { get; set; }

        [JsonProperty("public_keys")]
        public UserPublicKeys PublicKeys { get; set; }

        //todo: verifications

        [JsonProperty("newsletter")]
        public bool Newsletter { get; set; }

        [JsonProperty("announcements")]
        public bool Announcements { get; set; }

        [JsonProperty("activated")]
        public bool Activated { get; set; }

        [JsonProperty("sn")]
        public string SN { get; set; }

    }
}