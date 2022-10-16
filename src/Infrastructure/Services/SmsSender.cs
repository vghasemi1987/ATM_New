using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DomainContracts.Commons;
using DomainContracts.SettingAggregate;
using Newtonsoft.Json;

namespace Infrastructure.Services
{
    public class SmsSender : ISmsSender
    {
        private readonly ISettingRepository _settingRepository;

        public SmsSender(ISettingRepository settingRepository)
        {
            _settingRepository = settingRepository;
        }
        public async Task SendSmsAsync(List<string> phone, string message)
        {
            var smsOptions = await _settingRepository.FindByIdAsync(1);
            await Execute(phone, message, smsOptions.SmsUserName, smsOptions.SmsPassword, smsOptions.SmsServiceNumber);
            //return Task.FromResult(0);
        }
        private static async Task Execute(List<string> phone, string message, string userName, string password, string serviceNumber)
        {
            var request = WebRequest.Create("http://ippanel.com/services.jspd");
            var json = JsonConvert.SerializeObject(phone.ToArray());
            request.Method = "POST";
            var byteArray = Encoding.UTF8.GetBytes($"op=send&uname={userName}&pass={password}&message={message}&to={json}&from={serviceNumber}");
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            var dataStream = await request.GetRequestStreamAsync();
            await dataStream.WriteAsync(byteArray, 0, byteArray.Length);
            dataStream.Close();
            var response = await request.GetResponseAsync();
            //var aaa = ((HttpWebResponse)response).StatusDescription;
            var reader = new StreamReader(response.GetResponseStream());
            //var responseFromServer = reader.ReadToEnd();
            reader.Close();
            response.Close();
            //return responseFromServer;
        }
    }
}
