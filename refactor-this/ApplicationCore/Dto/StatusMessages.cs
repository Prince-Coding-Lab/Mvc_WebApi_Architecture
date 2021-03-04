using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Dto
{
	public class StatusMessages
	{
		public static string SuccessMessage = "Success";

		public static string FailureMessage = "Failed";

		public static string ServerError = "Server Error";

		public static string DomainValidationError = "Domain Validation Error";

		public static string MissingRequiredFields = "Required field missing";
	}
}
