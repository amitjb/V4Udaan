using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V4Udaan.Utils
{
    public static class MessageConstants
    {
        public static readonly string QuitApp = $"Are you sure you want to quit the Application?";
        public static readonly string UserNamePasswordRequired = $"Please enter Username/Password.";
        public static readonly string LoginSuccess = $"User login success.";
        public static readonly string LoginFailure = $"User login unsuccessful. Please try again.";
        public static string LoginFailureMessage(string message) => $"User login unsuccessfull due to {message}. Please try again.";
        public static readonly string SentTallyConnectedCompanySuccess = "Sent Tally Connected Companies Successfully.";
        public static readonly string SentTallyDayBookInformationSuccess = "Sent Tally DayBook Information Successfully.";
        public static readonly string SentTallyStatusSuccess = "Sent TallyStatus Successfully.";
        public static readonly string TallyConnected = "Tally is Connected.";
        public static readonly string TallyNotConnected = "Tally is not Connected.";
    }
}
