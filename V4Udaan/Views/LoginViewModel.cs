using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using V4Udaan.Api;
using V4Udaan.Api.Models;
using V4Udaan.Api.XML_Models;
using V4Udaan.Models;
using V4Udaan.Utils;

namespace V4Udaan.Views
{
    public class LoginViewModel : BaseViewModel
    {
        #region :: Attributes ::
        private string _userNm;
        public string UserNm
        {
            get { return _userNm; }
            set { SetProperty(ref _userNm, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
        #endregion
        #region :: NonAttributes ::
        #endregion
        #region :: Constructor ::
        public LoginViewModel()
        {
            initModel();
            CommandBinding();
        }
        #endregion
        #region :: Commands ::
        public ICommand LoginCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        #endregion
        #region :: Services :: 
        private void initModel()
        {
            //GlobalSettings.UserName = "nisarg.itlion@gmail.com";
            //GlobalSettings.Password = "123";
            //this._userNm = GlobalSettings.UserName;
            //this._password = GlobalSettings.Password;

            this._userNm = "nisarg.itlion@gmail.com";
            this._password = "123";
        }
        private void CommandBinding()
        {
            LoginCommand = new DelegateCommand(Login);
            CancelCommand = new DelegateCommand(Cancel);
        }
        #endregion
        #region :: Command Methods ::
        public async void Login(object param)
        {
            //var words = new[] { ",this", "   is   ", "a", "test", "Super, \"luxurious\" truck" };

            //System.Diagnostics.Debug.WriteLine(words.ToCsv(x => x, typeof(CsvAlwaysQuote<string>)));
            //System.Diagnostics.Debug.WriteLine(words.ToCsv(x => x, typeof(CsvRfc4180<string>)));
            //System.Diagnostics.Debug.WriteLine(words.ToCsv(x => x, typeof(CsvBare<string>)));
            //System.Diagnostics.Debug.WriteLine(words.ToCsv(x => x, typeof(CsvTrimAlwaysQuote<string>)));
            //System.Diagnostics.Debug.WriteLine(words.ToCsv(x => x, typeof(CsvTrimRfc4180<string>)));
            //System.Diagnostics.Debug.WriteLine(words.ToCsv(x => x, typeof(CsvTrimBare<string>)));

            //string str = "<ENVELOPE><COMPANYNAME.LIST><COMPANYNAME>Itlion</COMPANYNAME><COMPANYNAME>Itlion</COMPANYNAME><COMPANYNAME>Itlion</COMPANYNAME>" +
            //    "<COMPANYNAME>Itlion</COMPANYNAME></COMPANYNAME.LIST></ENVELOPE>";
            //ENVELOPE CompanyList = XmlHelper.Deserialize<ENVELOPE>(str);
            //System.Diagnostics.Debug.WriteLine(CompanyList.COMPANYNAME_LIST.COMPANYNAME.ToCsv(x => x, typeof(CsvTrimBare<string>)));

            if (this.UserNm == string.Empty || this.Password == string.Empty)
            {
                MessageBox.Show(MessageConstants.UserNamePasswordRequired, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                //bool resa= ApiManager.getInstance().CHECKCONNECTED();
                GlobalSettings.UserName = this.UserNm;
                GlobalSettings.Password = this.Password;
                LoginResquestModel loginResquestModel = new LoginResquestModel(this.UserNm, this.Password);
                ApiResponseWrapper<LoginResposeModel> res = await ApiManager.getInstance().Login(loginResquestModel);
                if (res == null || res.IsSuccessful == false)
                {
                    MessageBox.Show(MessageConstants.LoginFailureMessage("invalid credentials"), Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (res.Data.status)
                {
                    if (param != null)
                    {
                        Window win = param as Window;
                        //TempMainContainerView tempMainContainerView = new TempMainContainerView();
                        TempTestView tempMainContainerView = new TempTestView();
                        tempMainContainerView.Owner = win;
                        win.Hide();
                        tempMainContainerView.ShowDialog();
                    }
                    //MessageBox.Show(MessageConstants.LoginSuccess, Application.Current.Resources["AppTitle"].ToString(), MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        public void Cancel(object param)
        {
            MessageBoxResult res = MessageBox.Show(MessageConstants.QuitApp, Application.Current.Resources["AppTitle"].ToString(),
                                                   MessageBoxButton.YesNo, MessageBoxImage.Question);
            switch (res)
            {
                case MessageBoxResult.None:
                    break;
                case MessageBoxResult.OK:
                    break;
                case MessageBoxResult.Cancel:
                    break;
                case MessageBoxResult.Yes:
                    System.Windows.Application.Current.Shutdown();
                    break;
                case MessageBoxResult.No:
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}