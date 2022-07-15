using System.Text;

namespace NcmApi
{
    public static class Settings
    {
        public static string KEY = "c3f7fa686cd749c0a69f8b7f7241ef0e";
        public static byte[] KEY_BYTES => Encoding.ASCII.GetBytes(KEY);
    }
}
