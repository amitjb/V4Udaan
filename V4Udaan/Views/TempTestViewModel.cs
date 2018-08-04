using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using V4Udaan.Api;
using V4Udaan.Api.Models;
using V4Udaan.Api.XML_Models;
using V4Udaan.Models;
using V4Udaan.Utils;

namespace V4Udaan.Views
{
    public class TempTestViewModel : BaseViewModel
    {
        #region :: Attributes ::
        private string _lastRunOnStr;
        public string LastRunOnStr
        {
            get { return _lastRunOnStr; }
            set { SetProperty(ref _lastRunOnStr, value); }
        }
        private string _logStr;
        public string LogStr
        {
            get { return _logStr; }
            set { SetProperty(ref _logStr, value); }
        }
        private bool _isStartEnable;
        public bool IsStartEnable
        {
            get { return _isStartEnable; }
            set { SetProperty(ref _isStartEnable, value); }
        }
        private bool _isStopEnable;
        public bool IsStopEnable
        {
            get { return _isStopEnable; }
            set { SetProperty(ref _isStopEnable, value); }
        }
        //private string _tallyConnectedStr;
        //public string TallyConnectedStr
        //{
        //    get { return _tallyConnectedStr; }
        //    set { SetProperty(ref _tallyConnectedStr, value); }
        //}
        //private enumTallyConnectedStatus _tallyConnectedStatus;
        //public enumTallyConnectedStatus TallyConnectedStatus
        //{
        //    get { return _tallyConnectedStatus; }
        //    set
        //    {
        //        SetProperty(ref _tallyConnectedStatus, value);
        //        switch (value)
        //        {
        //            case enumTallyConnectedStatus.C:
        //                TallyConnectedStr = "Connected";
        //                break;
        //            case enumTallyConnectedStatus.NC:
        //                TallyConnectedStr = "Not Connected";
        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}
        //private string _tallyConnectedCompanyStr;
        //public string TallyConnectedCompanyStr
        //{
        //    get { return _tallyConnectedCompanyStr; }
        //    set { SetProperty(ref _tallyConnectedCompanyStr, value); }
        //}
        //private string _tallyDayBookInformationStr;
        //public string TallyDayBookInformationStr
        //{
        //    get { return _tallyDayBookInformationStr; }
        //    set { SetProperty(ref _tallyDayBookInformationStr, value); }
        //}
        #endregion
        #region :: NonAttributes ::
        DispatcherTimer timer;
        #endregion
        #region :: Constructor ::
        public TempTestViewModel()
        {
            initModel();
            CommandBinding();
        }
        #endregion
        #region :: Commands ::
        public ICommand StartServicesCommand { get; set; }
        public ICommand StopServicesCommand { get; set; }
        //public ICommand CheckTallyConnectedCommand { get; set; }
        //public ICommand SendTallyStatusToServerCommand { get; set; }
        //public ICommand CheckTallyConnectedCompaniesCommand { get; set; }
        //public ICommand SendTallyConnectedCompaniesToServerCommand { get; set; }
        //public ICommand CheckTallyDayBookInformationCommand { get; set; }
        //public ICommand SendTallyDayBookInformationCommand { get; set; }
        #endregion
        #region :: Services :: 
        private void initModel()
        {
            this._logStr = string.Empty;
            this._lastRunOnStr = string.Empty;
            this._isStartEnable = true;
            this._isStopEnable = false;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(10);
            timer.Tick += Timer_Tick;
            //this._tallyConnectedStr = "Not Connected";
            //this._tallyConnectedStatus = enumTallyConnectedStatus.NC;
            //this._tallyConnectedCompanyStr = string.Empty;
            //this._tallyDayBookInformationStr = string.Empty;
        }
        private void CommandBinding()
        {
            //CheckTallyConnectedCommand = new DelegateCommand(CheckTallyConnected);
            //SendTallyStatusToServerCommand = new DelegateCommand(SendTallyStatusToServer);
            //CheckTallyConnectedCompaniesCommand = new DelegateCommand(CheckTallyConnectedCompanies);
            //SendTallyConnectedCompaniesToServerCommand = new DelegateCommand(SendTallyConnectedCompaniesToServer);
            //CheckTallyDayBookInformationCommand = new DelegateCommand(CheckTallyDayBookInformation);
            //SendTallyDayBookInformationCommand = new DelegateCommand(SendTallyDayBookInformation);

            StartServicesCommand = new DelegateCommand(StartServices);
            StopServicesCommand = new DelegateCommand(StopServices);
        }
        private async Task<ApiResponseWrapper<string>> GetTallyConnected()
        {
            return await ApiManager.getInstance().SendTallyReqst(ApiConstants.XML_ListCompany);
        }
        private async Task<ApiResponseWrapper<string>> GetTallyDayBookInformation(string companyName)
        {
            return await ApiManager.getInstance().SendTallyReqst(ApiConstants.XML_DayBook(companyName));
        }
        private async Task<ApiResponseWrapper<string>> GetTallyRaportsInformation(string reportName, string companyName)
        {
            return await ApiManager.getInstance().SendTallyReqst(ApiConstants.XML_GetReport(reportName, companyName));
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            StartServices();
        }
        #endregion
        #region :: Command Methods ::
        public void AddEscapeSequence()
        {
            this.LogStr += "==========================================================\n";
        }
        public void StartServices(object param)
        {
            this.LogStr = " \n";
            this.LogStr += "Service Started. \n";
            AddEscapeSequence();
            StartServices();
        }
        public async void StartServices()
        {
            if (!timer.IsEnabled)
                timer.Start();
            this.IsStartEnable = false;
            this.IsStopEnable = true;
            this.LogStr += "Checking for Tally connection. \n";
            AddEscapeSequence();
            ApiResponseWrapper<string> res = await GetTallyConnected();
            if (!res.IsSuccessful)
            {
                this.LogStr += "Tally is not connected. Please start Tally and start the services again... \n";
                AddEscapeSequence();
                TallyConnectedResquestModel tallyConnectedResquestModel = new TallyConnectedResquestModel(GlobalSettings.UserName, enumTallyConnectedStatus.nc);
                ApiResponseWrapper<TallyConnectedResponseModel> res1 = await ApiManager.getInstance().SendTallyConnected(tallyConnectedResquestModel);
                if (!res1.IsSuccessful)
                {
                    this.LogStr += "Something went wrong :  \n" + res.Message;
                    AddEscapeSequence();
                }
                else
                {
                    this.LogStr += "Sent Tally status successfully to the server. \n";
                    AddEscapeSequence();
                }
            }
            else
            {
                AddEscapeSequence();
                this.LogStr += "Tally is connected. \n";
                AddEscapeSequence();
                this.LogStr += "Sending Tally connected confirmation to server. \n";
                AddEscapeSequence();
                TallyConnectedResquestModel tallyConnectedResquestModel = new TallyConnectedResquestModel(GlobalSettings.UserName, enumTallyConnectedStatus.c);
                ApiResponseWrapper<TallyConnectedResponseModel> res1 = await ApiManager.getInstance().SendTallyConnected(tallyConnectedResquestModel);
                if (!res1.IsSuccessful)
                {
                    this.LogStr += "Something went wrong :  \n" + res.Message;
                    AddEscapeSequence();
                }
                else
                {
                    this.LogStr += "Sent Tally status successfully to the server. \n";
                    AddEscapeSequence();
                    this.LogStr += "Checking for Tally connected companies. \n";
                    AddEscapeSequence();
                    ApiResponseWrapper<string> res2 = await GetTallyConnected();
                    if (res2.IsSuccessful)
                    {
                        this.LogStr += "Tally connected companies: \n" + res2.Data;
                        AddEscapeSequence();
                        this.LogStr += "Sending Tally connected companies to the server. \n";
                        AddEscapeSequence();
                        ENVELOPE CompanyListXmlObj = XmlHelper.Deserialize<ENVELOPE>(res2.Data);
                        string CompanyListCSV = (CompanyListXmlObj.COMPANYNAME_LIST.COMPANYNAME.ToCsv(x => x, typeof(CsvTrimBare<string>)));
                        TallyConnectedCompaniesResquestModel tallyConnectedCompaniesResquestModel = new TallyConnectedCompaniesResquestModel(GlobalSettings.UserName, CompanyListCSV);
                        ApiResponseWrapper<TallyConnectedCompaniesResponseModel> res3 = await ApiManager.getInstance().SendTallyConnectedCompanies(tallyConnectedCompaniesResquestModel);
                        if (!res3.IsSuccessful)
                        {
                            this.LogStr += "Something went wrong :  \n" + res3.Message;
                            AddEscapeSequence();
                        }
                        else
                        {
                            this.LogStr += "Sent connected companies successfully to the server. \n";
                            AddEscapeSequence();
                            if (CompanyListXmlObj.COMPANYNAME_LIST.COMPANYNAME.Count > 0)
                            {
                                foreach (var COMPANYNAME in CompanyListXmlObj.COMPANYNAME_LIST.COMPANYNAME)
                                {
                                    //===============================DayBook Summary=======================================
                                    this.LogStr += "Getting TallyDayBookInformation for " + COMPANYNAME + " company. \n";
                                    AddEscapeSequence();
                                    ApiResponseWrapper<string> res4 = await GetTallyRaportsInformation(ApiConstants.TallyReportName_DayBook, COMPANYNAME);
                                    //ApiResponseWrapper<string> res4 = await GetTallyDayBookInformation(COMPANYNAME);
                                    if (!res4.IsSuccessful)
                                    {
                                        this.LogStr += "Something went wrong :  \n" + res4.Message;
                                        AddEscapeSequence();
                                    }
                                    else
                                    {
                                        this.LogStr += COMPANYNAME + " TallyDayBookInformation:  \n" + res4.Data;
                                        AddEscapeSequence();
                                        this.LogStr += "Sending " + COMPANYNAME + " TallyDayBookInformation to the server. \n";
                                        AddEscapeSequence();
                                        //TallyDayBookInfoResquestModel tallyDayBookInfoResquestModel = new TallyDayBookInfoResquestModel(enumXMLType.DayBook, GlobalSettings.UserName, "ITLION", res4.Data);
                                        TallyDayBookInfoResquestModel tallyDayBookInfoResquestModel = new TallyDayBookInfoResquestModel(enumXMLType.daybook, GlobalSettings.UserName, COMPANYNAME, res4.Data);
                                        ApiResponseWrapper<TallyDayBookInfoResponseModel> res5 = await ApiManager.getInstance().SendTallyDayBookInformation(tallyDayBookInfoResquestModel);
                                        if (!res5.IsSuccessful)
                                        {
                                            this.LogStr += "Something went wrong :  \n" + res5.Message;
                                            AddEscapeSequence();
                                        }
                                        else
                                        {
                                            this.LogStr += "Sent " + COMPANYNAME + " TallyDayBookInformation successfully to the server. \n";
                                            AddEscapeSequence();
                                        }
                                    }
                                    //===============================Stock Summary=======================================
                                    this.LogStr += "Getting TallyStockCategorySummary Information for " + COMPANYNAME + " company. \n";
                                    AddEscapeSequence();
                                    ApiResponseWrapper<string> res6 = await GetTallyRaportsInformation(ApiConstants.TallyReportName_Stock, COMPANYNAME);
                                    if (!res6.IsSuccessful)
                                    {
                                        this.LogStr += "Something went wrong :  \n" + res6.Message;
                                        AddEscapeSequence();
                                    }
                                    else
                                    {
                                        this.LogStr += COMPANYNAME + " TallyStockCategorySummary Information \n" + res6.Data;
                                        AddEscapeSequence();
                                        this.LogStr += "Sending " + COMPANYNAME + " TallyStockCategorySummary Information to the server. \n";
                                        AddEscapeSequence();
                                        //TallyDayBookInfoResquestModel tallyDayBookInfoResquestModel = new TallyDayBookInfoResquestModel(enumXMLType.DayBook, GlobalSettings.UserName, "ITLION", res4.Data);
                                        TallyDayBookInfoResquestModel tallyDayBookInfoResquestModel = new TallyDayBookInfoResquestModel(enumXMLType.stock, GlobalSettings.UserName, COMPANYNAME, res6.Data);
                                        ApiResponseWrapper<TallyDayBookInfoResponseModel> res7 = await ApiManager.getInstance().SendTallyDayBookInformation(tallyDayBookInfoResquestModel);
                                        if (!res7.IsSuccessful)
                                        {
                                            this.LogStr += "Something went wrong :  \n" + res7.Message;
                                            AddEscapeSequence();
                                        }
                                        else
                                        {
                                            this.LogStr += "Sent " + COMPANYNAME + " TallyStockCategorySummary Information successfully to the server. \n";
                                            AddEscapeSequence();
                                        }
                                    }
                                }
                            }
                            else
                            {
                                this.LogStr += "Can not Send TallyDayBookInformation to the server due to unavailability of connected companies from Tally.\n";
                                AddEscapeSequence();
                            }
                        }
                    }
                }
            }
            this.LastRunOnStr = "Services Last excuted on: " + DateTime.Now.ToString();
        }
        public void StopServices(object param)
        {
            this.IsStartEnable = true;
            this.IsStopEnable = false;
            if (timer.IsEnabled)
                timer.Stop();
            this.LogStr += "Service Stopped. \n";
            AddEscapeSequence();
        }
        //public async void CheckTallyConnected(object param)
        //{
        //    ApiResponseWrapper<string> res = await GetTallyConnected();
        //    if (!res.IsSuccessful)
        //    {
        //        TallyConnectedStatus = enumTallyConnectedStatus.NC;
        //        MessageBox.Show(MessageConstants.TallyNotConnected, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //    else
        //    {
        //        MessageBox.Show(MessageConstants.TallyConnected, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
        //        TallyConnectedStatus = enumTallyConnectedStatus.C;
        //    }
        //}
        //public async void SendTallyStatusToServer(object param)
        //{
        //    TallyConnectedResquestModel tallyConnectedResquestModel = new TallyConnectedResquestModel(GlobalSettings.UserName, enumTallyConnectedStatus.NC);
        //    ApiResponseWrapper<TallyConnectedResponseModel> res = await ApiManager.getInstance().SendTallyConnected(tallyConnectedResquestModel);
        //    if (!res.IsSuccessful)
        //    {
        //        MessageBox.Show(res.Message, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //    else
        //    {
        //        MessageBox.Show(MessageConstants.SentTallyStatusSuccess, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //}
        //public async void CheckTallyConnectedCompanies(object param)
        //{
        //    if (TallyConnectedStatus == enumTallyConnectedStatus.C)
        //    {
        //        ApiResponseWrapper<string> res = await GetTallyConnected();
        //        if (res.IsSuccessful)
        //        {
        //            TallyConnectedCompanyStr = res.Data;
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(MessageConstants.TallyNotConnected, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //}
        //public async void SendTallyConnectedCompaniesToServer(object param)
        //{
        //    if (TallyConnectedStatus == enumTallyConnectedStatus.C)
        //    {
        //        ApiResponseWrapper<string> res = await GetTallyConnected();
        //        if (res.IsSuccessful)
        //        {
        //            TallyConnectedCompanyStr = res.Data;
        //            TallyConnectedCompaniesResquestModel tallyConnectedCompaniesResquestModel = new TallyConnectedCompaniesResquestModel(GlobalSettings.UserName, TallyConnectedCompanyStr);
        //            ApiResponseWrapper<TallyConnectedCompaniesResponseModel> res1 = await ApiManager.getInstance().SendTallyConnectedCompanies(tallyConnectedCompaniesResquestModel);
        //            if (!res1.IsSuccessful)
        //            {
        //                MessageBox.Show(res1.Message, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
        //            }
        //            else
        //            {
        //                MessageBox.Show(MessageConstants.SentTallyConnectedCompanySuccess, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(MessageConstants.TallyNotConnected, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //}
        //public async void CheckTallyDayBookInformation(object param)
        //{
        //    if (TallyConnectedStatus == enumTallyConnectedStatus.C)
        //    {
        //        ApiResponseWrapper<string> res = await GetTallyDayBookInformation();
        //        if (res.IsSuccessful)
        //        {
        //            TallyDayBookInformationStr = res.Data;
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(MessageConstants.TallyNotConnected, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //}
        //public async void SendTallyDayBookInformation(object param)
        //{
        //    if (TallyConnectedStatus == enumTallyConnectedStatus.C)
        //    {
        //        ApiResponseWrapper<string> res = await GetTallyDayBookInformation();
        //        if (res.IsSuccessful)
        //        {
        //            TallyDayBookInformationStr = res.Data;
        //            TallyDayBookInfoResquestModel tallyDayBookInfoResquestModel = new TallyDayBookInfoResquestModel(enumXMLType.DayBook, GlobalSettings.UserName, "ITLION", TallyDayBookInformationStr);
        //            ApiResponseWrapper<TallyDayBookInfoResponseModel> res1 = await ApiManager.getInstance().SendTallyDayBookInformation(tallyDayBookInfoResquestModel);
        //            if (!res1.IsSuccessful)
        //            {
        //                MessageBox.Show(res1.Message, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
        //            }
        //            else
        //            {
        //                MessageBox.Show(MessageConstants.SentTallyDayBookInformationSuccess, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
        //            }
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show(MessageConstants.TallyNotConnected, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //}
        #endregion
    }
}