
using Animal_Protection.Data;
using Protection_Animal.Model.Entities;
using System.Diagnostics.CodeAnalysis;

namespace StudentManager.WebApp.Models
{
    public class DbLogger : ILogger
    {
        /// <summary>
        /// Instance of <see cref="DbLoggerProvider" />.
        /// </summary>
        private readonly DbLoggerProvider _dbLoggerProvider;
        private readonly IServiceScopeFactory _scopeFactory;

        /// <summary>
        /// Creates a new instance of <see cref="FileLogger" />.
        /// </summary>
        /// <param name="fileLoggerProvider">Instance of <see cref="FileLoggerProvider" />.</param>
        public DbLogger([NotNull] DbLoggerProvider dbLoggerProvider,
            IServiceScopeFactory context)
        {
            _dbLoggerProvider = dbLoggerProvider;
            _scopeFactory = context;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// Whether to log the entry.
        /// </summary>
        /// <param name="logLevel"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }


        /// <summary>
        /// Used to log the entry.
        /// </summary>
        /// <typeparam name="TState"></typeparam>
        /// <param name="logLevel">An instance of <see cref="LogLevel"/>.</param>
        /// <param name="eventId">The event's ID. An instance of <see cref="EventId"/>.</param>
        /// <param name="state">The event's state.</param>
        /// <param name="exception">The event's exception. An instance of <see cref="Exception" /></param>
        /// <param name="formatter">A delegate that formats </param>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                // Don't log the entry if it's not enabled.
                return;
            }
            using var scope = _scopeFactory.CreateScope();

            // Get a Dbcontext from the scope
            var context = scope.ServiceProvider
                .GetRequiredService<AppDbContext>();

                context.Logs.Add(new LogModel()
            {
                Action = null,
                Controller = null,
                ExceptionMessage = exception.Message,
                Message = formatter(state, exception),
                StackTrace = exception.StackTrace
            });

            context.SaveChanges();
        }
    }
}
