

using LogMessageTest.Repo;
namespace LogMessageTest.Repo
{
	public class LogMessageRepo : ILogMessageRepo
	{
        private readonly ProceedxContext _context;

        public LogMessageRepo(ProceedxContext context)
		{
			_context= context;
		}

		public void CreateMessage(Logmessage logmessage)
		{

			logmessage.Application = _context.Applications.Where(x => x.Id == logmessage.ApplicationId).FirstOrDefault();
			_context.Add(logmessage);
			_context.SaveChanges();
		}
	}
}