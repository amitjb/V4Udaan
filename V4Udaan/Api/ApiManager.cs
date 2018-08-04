using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using V4Udaan.Api.Models;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;

namespace V4Udaan.Api
{
    public partial class ApiManager
    {
        private static ApiManager _instance;
        public static ApiManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ApiManager();
                return _instance;
            }
        }

        public static ApiManager getInstance()
        {
            return Instance;
        }

        public async Task<ApiResponseWrapper<LoginResposeModel>> Login(LoginResquestModel loginResquestModel)
        {
            ApiResponseWrapper<LoginResposeModel> res = new ApiResponseWrapper<LoginResposeModel>();
            try
            {
                HttpClient client = new HttpClient();
                //specify to use TLS 1.2 as default connection
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var values = new Dictionary<string, string>
                {
                    { "username", loginResquestModel.UserName },
                    { "password", loginResquestModel.Password }
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(ApiConstants.LoginApiUrl, content);
                string result = response.Content.ReadAsStringAsync().Result;
                response.Dispose();
                res = new ApiResponseWrapper<LoginResposeModel>(true, JsonConvert.DeserializeObject<LoginResposeModel>(result));
            }
            catch (Exception ex)
            {
                res = new ApiResponseWrapper<LoginResposeModel>(false, null, ex.Message);
            }
            return res;
        }

        public async Task<ApiResponseWrapper<TallyConnectedResponseModel>> SendTallyConnected(TallyConnectedResquestModel tallyConnectedResquestModel)
        {
            ApiResponseWrapper<TallyConnectedResponseModel> res = new ApiResponseWrapper<TallyConnectedResponseModel>();
            try
            {
                HttpClient client = new HttpClient();
                //specify to use TLS 1.2 as default connection
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var values = new Dictionary<string, string>
                {
                    { "login_id", tallyConnectedResquestModel.UserName },
                    { "status", tallyConnectedResquestModel.Status.ToString() }
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(ApiConstants.TallyStatusApiUrl, content);
                string result = response.Content.ReadAsStringAsync().Result;
                response.Dispose();
                res = new ApiResponseWrapper<TallyConnectedResponseModel>(true, JsonConvert.DeserializeObject<TallyConnectedResponseModel>(result));
            }
            catch (Exception ex)
            {
                res = new ApiResponseWrapper<TallyConnectedResponseModel>(false, null, ex.Message);
            }
            return res;
        }

        public async Task<ApiResponseWrapper<TallyConnectedCompaniesResponseModel>> SendTallyConnectedCompanies(TallyConnectedCompaniesResquestModel tallyConnectedCompaniesResquestModel)
        {
            ApiResponseWrapper<TallyConnectedCompaniesResponseModel> res = new ApiResponseWrapper<TallyConnectedCompaniesResponseModel>();
            try
            {
                HttpClient client = new HttpClient();
                //specify to use TLS 1.2 as default connection
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var values = new Dictionary<string, string>
                {
                    { "login_id", tallyConnectedCompaniesResquestModel.UserName },
                    { "company_name", tallyConnectedCompaniesResquestModel.CompanyNames }
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(ApiConstants.CompanyInsertApiUrl, content);
                string result = response.Content.ReadAsStringAsync().Result;
                response.Dispose();
                res = new ApiResponseWrapper<TallyConnectedCompaniesResponseModel>(true, JsonConvert.DeserializeObject<TallyConnectedCompaniesResponseModel>(result));
            }
            catch (Exception ex)
            {
                res = new ApiResponseWrapper<TallyConnectedCompaniesResponseModel>(false, null, ex.Message);
            }
            return res;
        }

        public async Task<ApiResponseWrapper<TallyDayBookInfoResponseModel>> SendTallyDayBookInformation(TallyDayBookInfoResquestModel tallyDayBookInfoResquestModel)
        {
            ApiResponseWrapper<TallyDayBookInfoResponseModel> res = new ApiResponseWrapper<TallyDayBookInfoResponseModel>();
            try
            {
                HttpClient client = new HttpClient();
                //specify to use TLS 1.2 as default connection
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                var values = new Dictionary<string, string>
                {
                    { "login_id", tallyDayBookInfoResquestModel.UserName },
                    { "company_name", tallyDayBookInfoResquestModel.CompanyName },
                    { "xml_type", tallyDayBookInfoResquestModel.XML_Type.ToString() },
                    { "xml", tallyDayBookInfoResquestModel.XML }
                };
                var content = new FormUrlEncodedContent(values);
                var response = await client.PostAsync(ApiConstants.XmlInsertApiUrl, content);
                string result = response.Content.ReadAsStringAsync().Result;
                response.Dispose();
                res = new ApiResponseWrapper<TallyDayBookInfoResponseModel>(true, JsonConvert.DeserializeObject<TallyDayBookInfoResponseModel>(result));
            }
            catch (Exception ex)
            {
                res = new ApiResponseWrapper<TallyDayBookInfoResponseModel>(false, null, ex.Message);
            }
            return res;
        }
    }
    public partial class ApiManager
    {
        public async Task<ApiResponseWrapper<string>> SendTallyReqst(string webRequstXMLStr)
        {
            ApiResponseWrapper<string> res = new ApiResponseWrapper<string>();
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(ApiConstants.TallyBaseURL);
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = (long)webRequstXMLStr.Length;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                StreamWriter strmWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                strmWriter.Write(webRequstXMLStr);
                strmWriter.Close();

                //Normal Execution
                //HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //Stream receiveStream = httpResponse.GetResponseStream();
                //StreamReader streamReader = new StreamReader(receiveStream, Encoding.UTF8);
                //string responseStr = streamReader.ReadToEnd();
                //httpResponse.Close();
                //streamReader.Close();
                //res = new ApiResponseWrapper<string>(true, responseStr);

                //Async way
                WebResponse webResponse = await httpWebRequest.GetResponseAsync();
                Stream receiveStream = webResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(receiveStream, Encoding.UTF8);
                string responseStr = streamReader.ReadToEnd();
                webResponse.Close();
                streamReader.Close();
                res = new ApiResponseWrapper<string>(true, responseStr);
            }
            catch (Exception ex)
            {
                res = new ApiResponseWrapper<string>(false, null, ex.Message);
            }
            return res;
        }
    }
}