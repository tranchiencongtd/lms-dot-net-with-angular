using System.Security.Cryptography;
using System.Text;

namespace HDNXUdemyServices.CommonFunction
{
    public static class Generator
    {
        private static readonly Random Rand = new();

        public static string GenerateCodeTracker(string controllerName, string actionName, string pKey)
        {
            return GenerateCodeTracking(controllerName, actionName, pKey, 9);
        }

        private static string GenerateCodeTracking(string controllerName, string actionName, string pKey, int pNumberPart)
        {
            if (string.IsNullOrEmpty(pKey))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(controllerName))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(actionName))
            {
                return string.Empty;
            }

            pKey = pKey.ToUpper();
            controllerName = controllerName.ToUpper();
            actionName = actionName.ToUpper();

            return pKey + "." + controllerName + "." + actionName.PadLeft(pNumberPart, '0');
        }

        public static string GeneratePassword(int length)
        {
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string number = "1234567890";
            const string special = "!@#$%^&*_-=+";

            // Get cryptographically random sequence of bytes
            var bytes = new byte[length];
            new RNGCryptoServiceProvider().GetBytes(bytes);

            // Build up a string using random bytes and character classes
            var res = new StringBuilder();
            foreach (byte b in bytes)
            {
                // Randomly select a character class for each byte
                if (Rand.Next(4) == 0)
                {
                    res.Append(lower[b % lower.Length]);
                }
                else if (Rand.Next(4) == 1)
                {
                    res.Append(upper[b % upper.Length]);
                }
                else if (Rand.Next(4) == 2)
                {
                    res.Append(number[b % number.Length]);
                }
                else if (Rand.Next(4) == 3)
                {
                    res.Append(special[b % special.Length]);
                }
            }

            return res.ToString();
        }

        public static string ColorOfClassGroup(string codeClassGroup)
        {
            string returnValue;
            switch (codeClassGroup)
            {
                case "nganhau":
                    returnValue = "#00FF00";
                    break;

                case "nganhthieu":
                    returnValue = "#2F75B5";
                    break;

                case "nganhnghia":
                    returnValue = "#FFFF00";
                    break;

                case "vaodoi":
                    returnValue = "#BF8F00";
                    break;

                default:
                    returnValue = "#0EBB05";
                    break;
            }

            return returnValue;
        }

        public static string GenerateRandomString(long idStudent)
        {
            Random random = new Random();
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            char[] result = new char[5];
            for (int i = 0; i < result.Length; i++)
            {
                int randomIndex = random.Next(allowedChars.Length);
                result[i] = allowedChars[randomIndex];
            }
            var returnValue = new string(result);
            return $"HDNX{idStudent}{returnValue}";
        }
    }
}