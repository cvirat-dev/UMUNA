
namespace UMUNA.Data.DTOs
{
    [System.Serializable]
    public class UserDataDto
    {
        public string userName;
        public string userId;
        public string userEmail;
        public string userPassword; // This comes from server; in real apps, never expose passwords like this!
    }
}
