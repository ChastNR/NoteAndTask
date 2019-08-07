//using System.Text;
//using Microsoft.Extensions.Options;
//using TrampolineSPA.Extensions.EmailSender;
//using Twilio;
//using Twilio.Rest.Api.V2010.Account;
//using Twilio.Types;

//namespace NoteAndTask.Extensions.SmsSender
//{
//    public class SmsSender : ISmsSender
//    {
//        public SmsSettings SmsSettings { get; }

//        public SmsSender(IOptions<SmsSettings> smsSettings)
//        {
//            SmsSettings = smsSettings.Value;
//        }

//        public void SendMessage(PhoneNumber clientNumber, string smsBody)
//        {
//            TwilioClient.Init(SmsSettings.AccountSid, SmsSettings.AuthToken);

//            MessageResource.Create(
//                to: clientNumber,
//                from: new PhoneNumber(SmsSettings.PhoneNumber),
//                body: smsBody);
//        }

//        public void SendSmsInstructions(string service, string phoneNumber, string password)
//        {
//            string theme = "Инструкция по проверке вашего аккаунта";
//            string message = "Привет! Спасибо, что приобрели услугу: " + service + "!" +
//                             "\nЧтобы активировать услугу - войдите Ваш аккаунт:\n" +
//                             "\nВаш персональный пароль: " + password;

//            StringBuilder body = new StringBuilder();
//            body.Append(theme);
//            body.AppendLine();
//            body.Append(message);

//            SendMessage(phoneNumber, body.ToString());
//        }
//    }
//}
