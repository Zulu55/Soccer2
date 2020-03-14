using Soccer.Common.Models;

namespace Soccer.Web.Helpers
{
	public interface IMailHelper
	{
		Response SendMail(string to, string subject, string body);
	}
}
