using CloudinaryDotNet;

namespace PharmacyManagermentSystem.Properties
{
    public class CloudinaryConfig
    {
        public static Cloudinary GetCloudinaryInstance()
        {
            var account = new Account(
                "dy59z0ubm", // Cloud name
                "451974367355919", // API key
                "ivNawFdEk8BtmItqQ9ai1mHRRs4" // API secret
            );
            return new Cloudinary(account);
        } 
    }
}
