using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using V4Udaan.Models;
using V4Udaan.Utils;

namespace V4Udaan.Views
{
    public class MainContainerViewModel : BaseViewModel
    {
        #region :: Attributes ::
        private string _userNm;
        public string UserNm
        {
            get { return _userNm; }
            set { SetProperty(ref _userNm, value); }
        }
        #endregion
        #region :: NonAttributes ::
        #endregion
        #region :: Constructor ::
        public MainContainerViewModel()
        {
            initModel();
            CommandBinding();
        }
        #endregion
        #region :: Commands ::
        public ICommand WindowCloseCommand { get; set; }
        #endregion
        #region :: Services :: 
        private void initModel()
        {
            //this._userNm = string.Empty;
        }
        private void CommandBinding()
        {
            WindowCloseCommand = new DelegateCommand(Cancel);
        }
        #endregion
        #region :: Command Methods ::
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