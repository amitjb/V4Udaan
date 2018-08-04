using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using V4Udaan.Api;
using V4Udaan.Api.Models;
using V4Udaan.Models;
using V4Udaan.Utils;

namespace V4Udaan.Views
{
    public class TempMainContainerViewModel : BaseViewModel
    {
        #region :: Attributes ::
        private string _tallyConnectedStr;
        public string TallyConnectedStr
        {
            get { return _tallyConnectedStr; }
            set { SetProperty(ref _tallyConnectedStr, value); }
        }
        private enumTallyConnectedStatus _tallyConnectedStatus;
        public enumTallyConnectedStatus TallyConnectedStatus
        {
            get { return _tallyConnectedStatus; }
            set
            {
                SetProperty(ref _tallyConnectedStatus, value);
                switch (value)
                {
                    case enumTallyConnectedStatus.c:
                        TallyConnectedStr = "Connected";
                        break;
                    case enumTallyConnectedStatus.nc:
                        TallyConnectedStr = "Not Connected";
                        break;
                    default:
                        break;
                }
            }
        }
        private string _tallyConnectedCompanyStr;
        public string TallyConnectedCompanyStr
        {
            get { return _tallyConnectedCompanyStr; }
            set { SetProperty(ref _tallyConnectedCompanyStr, value); }
        }
        private string _tallyDayBookInformationStr;
        public string TallyDayBookInformationStr
        {
            get { return _tallyDayBookInformationStr; }
            set { SetProperty(ref _tallyDayBookInformationStr, value); }
        }
        #endregion
        #region :: NonAttributes ::
        #endregion
        #region :: Constructor ::
        public TempMainContainerViewModel()
        {
            initModel();
            CommandBinding();
        }
        #endregion
        #region :: Commands ::
        public ICommand CheckTallyConnectedCommand { get; set; }
        public ICommand SendTallyStatusToServerCommand { get; set; }
        public ICommand CheckTallyConnectedCompaniesCommand { get; set; }
        public ICommand SendTallyConnectedCompaniesToServerCommand { get; set; }
        public ICommand CheckTallyDayBookInformationCommand { get; set; }
        public ICommand SendTallyDayBookInformationCommand { get; set; }
        #endregion
        #region :: Services :: 
        private void initModel()
        {
            this._tallyConnectedStr = "Not Connected";
            this._tallyConnectedStatus = enumTallyConnectedStatus.nc;
            this._tallyConnectedCompanyStr = string.Empty;
            this._tallyDayBookInformationStr = string.Empty;
        }
        private void CommandBinding()
        {
            CheckTallyConnectedCommand = new DelegateCommand(CheckTallyConnected);
            SendTallyStatusToServerCommand = new DelegateCommand(SendTallyStatusToServer);
            CheckTallyConnectedCompaniesCommand = new DelegateCommand(CheckTallyConnectedCompanies);
            SendTallyConnectedCompaniesToServerCommand = new DelegateCommand(SendTallyConnectedCompaniesToServer);
            CheckTallyDayBookInformationCommand = new DelegateCommand(CheckTallyDayBookInformation);
            SendTallyDayBookInformationCommand = new DelegateCommand(SendTallyDayBookInformation);
        }
        private async Task<ApiResponseWrapper<string>> GetTallyConnected()
        {
            return await ApiManager.getInstance().SendTallyReqst(ApiConstants.XML_ListCompany);
        }
        private async Task<ApiResponseWrapper<string>> GetTallyDayBookInformation(string companyName)
        {
            return await ApiManager.getInstance().SendTallyReqst(ApiConstants.XML_DayBook(companyName));
        }
        #endregion
        #region :: Command Methods ::
        public async void CheckTallyConnected(object param)
        {
            ApiResponseWrapper<string> res = await GetTallyConnected();
            if (!res.IsSuccessful)
            {
                TallyConnectedStatus = enumTallyConnectedStatus.nc;
                MessageBox.Show(MessageConstants.TallyNotConnected, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show(MessageConstants.TallyConnected, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
                TallyConnectedStatus = enumTallyConnectedStatus.c;
            }
        }
        public async void SendTallyStatusToServer(object param)
        {
            TallyConnectedResquestModel tallyConnectedResquestModel = new TallyConnectedResquestModel(GlobalSettings.UserName, enumTallyConnectedStatus.nc);
            ApiResponseWrapper<TallyConnectedResponseModel> res = await ApiManager.getInstance().SendTallyConnected(tallyConnectedResquestModel);
            if (!res.IsSuccessful)
            {
                MessageBox.Show(res.Message, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                MessageBox.Show(MessageConstants.SentTallyStatusSuccess, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public async void CheckTallyConnectedCompanies(object param)
        {
            if (TallyConnectedStatus == enumTallyConnectedStatus.c)
            {
                ApiResponseWrapper<string> res = await GetTallyConnected();
                if (res.IsSuccessful)
                {
                    TallyConnectedCompanyStr = res.Data;
                }
            }
            else
            {
                MessageBox.Show(MessageConstants.TallyNotConnected, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public async void SendTallyConnectedCompaniesToServer(object param)
        {
            if (TallyConnectedStatus == enumTallyConnectedStatus.c)
            {
                ApiResponseWrapper<string> res = await GetTallyConnected();
                if (res.IsSuccessful)
                {
                    TallyConnectedCompanyStr = res.Data;
                    TallyConnectedCompaniesResquestModel tallyConnectedCompaniesResquestModel = new TallyConnectedCompaniesResquestModel(GlobalSettings.UserName, TallyConnectedCompanyStr);
                    ApiResponseWrapper<TallyConnectedCompaniesResponseModel> res1 = await ApiManager.getInstance().SendTallyConnectedCompanies(tallyConnectedCompaniesResquestModel);
                    if (!res1.IsSuccessful)
                    {
                        MessageBox.Show(res1.Message, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBox.Show(MessageConstants.SentTallyConnectedCompanySuccess, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show(MessageConstants.TallyNotConnected, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public async void CheckTallyDayBookInformation(object param)
        {
            if (TallyConnectedStatus == enumTallyConnectedStatus.c)
            {
                ApiResponseWrapper<string> res = await GetTallyDayBookInformation("ITLION");
                if (res.IsSuccessful)
                {
                    TallyDayBookInformationStr = res.Data;
                }
            }
            else
            {
                MessageBox.Show(MessageConstants.TallyNotConnected, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public async void SendTallyDayBookInformation(object param)
        {
            if (TallyConnectedStatus == enumTallyConnectedStatus.c)
            {
                ApiResponseWrapper<string> res = await GetTallyDayBookInformation("ITLION");
                if (res.IsSuccessful)
                {
                    TallyDayBookInformationStr = res.Data;
                    TallyDayBookInfoResquestModel tallyDayBookInfoResquestModel = new TallyDayBookInfoResquestModel(enumXMLType.daybook,GlobalSettings.UserName,"ITLION",TallyDayBookInformationStr);
                    ApiResponseWrapper<TallyDayBookInfoResponseModel> res1 = await ApiManager.getInstance().SendTallyDayBookInformation(tallyDayBookInfoResquestModel);
                    if (!res1.IsSuccessful)
                    {
                        MessageBox.Show(res1.Message, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        MessageBox.Show(MessageConstants.SentTallyDayBookInformationSuccess, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
            else
            {
                MessageBox.Show(MessageConstants.TallyNotConnected, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion
    }
}