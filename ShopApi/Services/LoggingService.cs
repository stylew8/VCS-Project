namespace ShopApi.Services
{
    public class LoggingService
    {
        public void Write(string message)
        {
            using (var streamWriter = new StreamWriter($"logs{DateTime.Now.ToString("yyyy-MMdd")}.txt", true))
            {
                streamWriter.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}: {message}");
            }
        }
    }
}
