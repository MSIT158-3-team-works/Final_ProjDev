namespace projFitConnect.ViewModels
{
    public class C_googleLoginProperty
    {
        public string localId {  get; set; }
        public string email { get; set; }
        public string displayName { get; set; }
        public string language { get; set; }
        public string photoUrl { get; set; }
        public string timeZone { get; set; }
        public string dateOfBirth { get; set; }
        public string passwordHash { get; set; }
        public string salt { get; set; }
        public string version { get; set; }
        public string emailVerified { get; set; }
        public string passwordUpdatedAt { get; set; }
        public List<object> providerUserInfo { get; set; }
        public string validSince{ get; set; }
        public bool disabled { get; set; }
        public string lastLoginAt { get; set; }
        public string createdAt { get; set; }
        public string screenName { get; set; }
        public bool customAuth { get; set; }
        public string rawPassword { get; set; }
        public string phoneNumber { get; set; }
        public string customAttributes { get; set; }
        public bool emailLinkSignin { get; set; }
        public string tenantId { get; set; }
        public List<object> mfaInfo { get; set; }
        public string initialEmail { get; set; }
        public string lastRefreshAt { get; set; }
    }
}
