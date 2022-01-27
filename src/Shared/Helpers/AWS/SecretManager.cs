using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System;
using System.IO;
using System.Text;

namespace Shared.Helpers.AWS
{
    internal static class SecretManager
    {
        public static string GetSecret(string secretName, string region)
        {
            MemoryStream memoryStream = new();

            IAmazonSecretsManager client = new AmazonSecretsManagerClient(RegionEndpoint.GetBySystemName(region));

            GetSecretValueRequest request = new();
            request.SecretId = secretName;
            request.VersionStage = "AWSCURRENT"; // VersionStage defaults to AWSCURRENT if unspecified.

            GetSecretValueResponse response;
            try
            {
                response = client.GetSecretValueAsync(request).Result;
            }
            catch (DecryptionFailureException e)
            {
                // Secrets Manager can't decrypt the protected secret text using the provided KMS key.
                throw e;
            }
            catch (InternalServiceErrorException e)
            {
                // An error occurred on the server side.
                throw e;
            }
            catch (InvalidParameterException e)
            {
                // You provided an invalid value for a parameter.
                throw e;
            }
            catch (InvalidRequestException e)
            {
                // You provided a parameter value that is not valid for the current state of the resource.
                throw e;
            }
            catch (ResourceNotFoundException e)
            {
                // We can't find the resource that you asked for.
                throw e;
            }
            catch (AggregateException ae)
            {
                // More than one of the above exceptions were triggered.
                throw ae;
            }

            string secret;
            // Decrypts secret using the associated KMS key.
            // Depending on whether the secret is a string or binary, one of these fields will be populated.
            if (response.SecretString != null)
            {
                secret = response.SecretString;
            }
            else
            {
                memoryStream = response.SecretBinary;
                StreamReader reader = new(memoryStream);
                string decodedBinarySecret = Encoding.UTF8.GetString(Convert.FromBase64String(reader.ReadToEnd()));
                secret = decodedBinarySecret;
            }

            return secret;
        }
    }
}
