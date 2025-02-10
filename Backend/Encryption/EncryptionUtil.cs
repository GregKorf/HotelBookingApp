namespace HotelBookingApp.Encryption
{
    public static class EncryptionUtil
    {
        public static string EncryptPassword(string password) 
        {
            var encryptedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return encryptedPassword;
        }

        public static bool VerifyPassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }
    }
}
