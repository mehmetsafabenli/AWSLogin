using Amazon.CognitoIdentityProvider.Model;

namespace Mbis.Cognito.Extensions
{
    public static class ObjectExtensions
    {
        public static void AddAttribute(this SignUpRequest request,
            Dictionary<string, string> attributes)
        {
            foreach (var attribute in attributes)
            {
                request.UserAttributes.Add(new AttributeType
                {
                    Name = attribute.Key,
                    Value = attribute.Value
                });
            }
        }
    }
}
