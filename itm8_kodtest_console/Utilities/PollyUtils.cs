using Polly;
using Polly.Retry;

namespace itm8_kodtest_console.Utilities
{
    public static class PollyUtils
    {
        public static  AsyncRetryPolicy PollyAsyncRetry()
        {
            var attempts = 3;
         AsyncRetryPolicy retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(attempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)), (exception, timeSpan, retryCount, context) =>
            {
                Console.WriteLine($"Retry number {retryCount}/{attempts}\nRetrying again in {timeSpan.Seconds} second");
            });
            
            return retryPolicy;
        }
    }
}