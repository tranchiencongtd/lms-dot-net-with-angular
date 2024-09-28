using HDNXUdemyModel.Base;
using HDNXUdemyModel.ResponModel;
using NodaTime;
using NodaTime.Text;
using System.Globalization;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace HDNXUdemyServices.CommonFunction
{
    public static class ProjectStringExtensions
    {
        private const int Keysize = 128;

        private const int DerivationIterations = 1000;

        public static string ToTitle(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return string.Empty;
            }

            List<string> words = inputString.Split(new[] { ' ' }).ToList();

            for (int i = words.Count - 1; i >= 0; i--)
            {
                if (words[i].Length > 0)
                {
                    words[i] = words[i][..1].ToUpper() + words[i][1..];
                }
                else
                {
                    words.RemoveAt(i);
                }
            }

            return string.Join(" ", words.ToArray());
        }

        public static String AllTrim(this String inputString)
        {
            return Regex.Replace(inputString, @"\s", String.Empty);
        }

        public static String ToProper(this String inputString)
        {
            string rText = inputString.ToLower();
            try
            {
                CultureInfo cultureInfo =
                System.Threading.Thread.CurrentThread.CurrentCulture;
                TextInfo textInfo = cultureInfo.TextInfo;
                rText = textInfo.ToTitleCase(rText);
            }
            catch
            {
                rText = inputString;
            }

            return rText;
        }

        public static String Mid(this String inputString, int startIndex, int subLen)
        {
            startIndex = Math.Min(Math.Max(0, startIndex), inputString.Length);
            subLen = Math.Min(Math.Max(0, subLen), inputString.Length);
            return inputString.Substring(startIndex, subLen);
        }

        public static String Left(this String inputString, int nlen)
        {
            int subLen = Math.Min(Math.Max(0, nlen), inputString.Length);
            return inputString[..subLen];
        }

        public static String Right(this String inputString, int nlen)
        {
            int startIndex = Math.Max(0, inputString.Length - Math.Max(0, nlen));
            return inputString[startIndex..];
        }

        public static String RighRemain(this String sstring, int nEat)
        {
            int startIndex = Math.Min(sstring.Length, Math.Max(0, nEat));
            return sstring[startIndex..];
        }

        public static String LeftRemain(this String inputString, int nEat)
        {
            int subLen = Math.Max(0, inputString.Length - Math.Max(0, nEat));
            return inputString[..subLen];
        }

        public static string NCharToChar(this string inputString)
        {
            Regex regex = new(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = inputString.Normalize(NormalizationForm.FormD);
            string sreturn = regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            sreturn = Regex.Replace(sreturn, "['`]", String.Empty);
            sreturn = Regex.Replace(sreturn, @"[^\w\s]", " ");
            sreturn = Regex.Replace(sreturn, @"[\s]+", " ");
            return sreturn;
        }

        public static string NCharToCamel(this string inputString)
        {
            string sreturn = inputString.NCharToChar();
            sreturn = sreturn.ToProper();
            sreturn = sreturn.AllTrim();
            return sreturn;
        }

        public static int EndsWithNum(this string inputString, out int iNum)
        {
            // Compile the regular expression.
            Regex r = new(@"(\d+)$");
            Match m = r.Match(inputString);
            if (m.Success)
            {
                iNum = Convert.ToInt32(m.Value);
                return m.Value.Length;
            }

            iNum = int.MinValue;
            return 0;
        }

        public static int StartsWithNum(this string inputString, out int iNum)
        {
            // Compile the regular expression.
            Regex r = new(@"^(\d+)");
            Match m = r.Match(inputString);
            if (m.Success)
            {
                iNum = Convert.ToInt32(m.Value);
                return m.Value.Length;
            }

            iNum = int.MinValue;
            return 0;
        }

        public static bool CheckPatternDate(this string inputString, out DateTime oDateValue)
        {
            string[] formats = { "yyyy/MM/dd", "yyyy/M/d", "dd/MM/yyyy", "d/M/yyyy", "MM/dd/yyyy", "M/d/yyyy" };
            bool result = false;

            oDateValue = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day);

            foreach (var item in formats)
            {
                if (DateTime.TryParseExact(inputString, item,
                                        CultureInfo.InvariantCulture,
                                        DateTimeStyles.None,
                                        out DateTime dateValue))
                {
                    oDateValue = dateValue;
                    result = true;
                    break;
                }
            }

            return result;
        }

        public static string Change_AV(this string inputString)
        {
            Regex vRegRegex = new("\\p{IsCombiningDiacriticalMarks}+");
            string vStrFormD = inputString.Normalize(NormalizationForm.FormD);
            return vRegRegex.Replace(vStrFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string RemoveSpecialCharacters(this string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                Regex re = new(ProjectConfig.SpecialCharacters ?? string.Empty);
                string outputString = re.Replace(inputString, "");
                return outputString;
            }
            else
            {
                return string.Empty;
            }
        }

        public static bool IsValidSpecialCharacters(this string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                return Regex.IsMatch(inputString, ProjectConfig.SpecialCharacters ?? String.Empty);
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidEmail(this string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                return Regex.IsMatch(inputString, ProjectConfig.EmailFormat ?? String.Empty);
            }
            else
            {
                return false;
            }
        }

        public static bool IsValidNumbers(this string inputString)
        {
            if (!string.IsNullOrEmpty(inputString))
            {
                return Regex.IsMatch(inputString, ProjectConfig.NumberPatternFormat ?? string.Empty);
            }
            else
            {
                return false;
            }
        }

        public static string Md5(this string inputString)
        {
            MD5 md5 = MD5.Create();
            byte[] input = Encoding.Default.GetBytes(inputString);
            byte[] output = md5.ComputeHash(input);
            return BitConverter.ToString(output).Replace("-", "");
        }

        public static string Encrypt(this string pText, string pKey)
        {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.
            var saltStringBytes = Generate128BitsOfRandomEntropy();
            var ivStringBytes = Generate128BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(pText);
            using var password = new Rfc2898DeriveBytes(pKey, saltStringBytes, DerivationIterations);
            var keyBytes = password.GetBytes(Keysize / 8);
            using var symmetricKey = new RijndaelManaged
            {
                BlockSize = 128,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
            using var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes);
            using var memoryStream = new MemoryStream();
            using var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();

            // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
            var cipherTextBytes = saltStringBytes;
            cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
            cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static string Decrypt(this string pText, string pKey)
        {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(pText);

            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();

            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();

            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using var password = new Rfc2898DeriveBytes(pKey, saltStringBytes, DerivationIterations);
            var keyBytes = password.GetBytes(Keysize / 8);
            using var symmetricKey = new RijndaelManaged
            {
                BlockSize = 128,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.PKCS7
            };
            using var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes);
            using var memoryStream = new MemoryStream(cipherTextBytes);
            using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
            var plainTextBytes = new byte[cipherTextBytes.Length];
            var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
            memoryStream.Close();
            cryptoStream.Close();
            return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        public static string FormatEmail(this string valueInput, params Expression<Func<string, object>>[] args)
        {
            var parameters = args.ToDictionary(e => string.Format("{{{0}}}", e.Parameters[0].Name), e => e.Compile()(e.Parameters[0].Name ?? ""));

            var sb = new StringBuilder(valueInput);
            foreach (var kv in parameters)
            {
                sb.Replace(kv.Key, kv.Value != null ? kv.Value.ToString() : "");
            }

            return sb.ToString();
        }

        public static LocalDateTime ChangeValueToDateTime(this string? valueInput)
        {
            if (string.IsNullOrEmpty(valueInput))
            {
                var valueData = LocalDate.FromDateTime(DateTime.UtcNow);
                return valueData.AtMidnight();
            }

            string pattern = "yyyy-MM-dd";
            LocalDate date = LocalDatePattern.CreateWithInvariantCulture(pattern).Parse(valueInput).GetValueOrThrow();
            return date.AtMidnight();
        }

        private static byte[] Generate128BitsOfRandomEntropy()
        {
            var randomBytes = new byte[16]; // 16 Bytes will give us 128 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }

            return randomBytes;
        }

        public static List<ListTypeFileUpload> GetEnumList<T>()
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("T must be an enumerated type");
            }

            return Enum.GetValues(typeof(T)).Cast<Enum>()
                .Select(value => new ListTypeFileUpload
                {
                    IdTypeFile = (int)Convert.ChangeType(value, typeof(int)),
                    NameOfFileType = value.ToString(),
                    NameTypeFileType = value.GetEnumDescription()
                })
                .ToList();
        }
    }
}