using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BankSystem.Services
{
    public class SmsService
    {
        public bool SendMessage(string mobileNumber, string message)
        {
            try
            {
                const string accountSid = "AC0b22ed5497eaad2ddb33bfb9adc1423d";
                const string authToken = "5c6dc9129c5e6b33e05213981fdc33c4";

                TwilioClient.Init(accountSid, authToken);

                var messageResource = MessageResource.Create(
                    body: message,
                    from: new Twilio.Types.PhoneNumber("+15852964551"),
                    to: new Twilio.Types.PhoneNumber(mobileNumber)
                );
                return true;
            }
            catch(Exception exc)
            {
                return false;
            }
        }
    }
}
