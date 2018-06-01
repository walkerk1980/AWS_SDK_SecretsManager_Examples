using System;
using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Threading.Tasks;
using System.Threading;
namespace SecretsManagerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var secretName = "TestSecret1";
            var config = new AmazonSecretsManagerConfig();
            config.RegionEndpoint = RegionEndpoint.USWest2;
            var client = new AmazonSecretsManagerClient(config);
            var secretValueRequest = new GetSecretValueRequest();
            secretValueRequest.SecretId = secretName;
            var secretValueResponse = GetSecretValueResponseAsync(client, secretValueRequest);
            
            if (secretValueResponse.Result.SecretString != null)
            {
                Console.WriteLine("Secret String: " + secretValueResponse.Result.SecretString);
            }
            else if (secretValueResponse.Result.SecretBinary !=null)
            {
                Console.WriteLine("SecretBinary saved to variable.");
                var secretBinary = secretValueResponse.Result.SecretBinary;
                //Do something with the SecretBinary in your code
            }
            else
            {
                Console.WriteLine("Secret String and Secret Binary are null.");
                //Do something with the SecretString in your code
            }
        }
        public static async Task<GetSecretValueResponse> GetSecretValueResponseAsync(AmazonSecretsManagerClient client, GetSecretValueRequest request)
        {
            try
            {
                var result = await client.GetSecretValueAsync(request);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                var result = new GetSecretValueResponse();
                return result;
            }
        }
    }
}
