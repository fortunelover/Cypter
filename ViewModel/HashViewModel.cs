using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Security.Cryptography;
using Cypter.Helper;

namespace Cypter.ViewModel
{
    public class HashViewModel: ObservableObject
    {
        private string _cipherModeStr;

        public string CipherModeStr
        {
            get => _cipherModeStr;
            set => SetProperty(ref _cipherModeStr, value);
        }

        private string _source;

        public string Source
        {
            get => _source;
            set => SetProperty(ref _source, value);
        }


        private string _destination;

        public string Destination
        {
            get => _destination;
            set => SetProperty(ref _destination, value);
        }


        public ICommand exe => new RelayCommand(() =>
        {
            try
            {
                switch (CipherModeStr)
                {
                    case "MD5":
                        Destination = HashHelper.GetMD5(Source);
                        break;
                    case "SHA1":
                        Destination = HashHelper.GetSHA1(Source);
                        break;
                    case "SM3":
                        Destination = HashHelper.GetSM3(Source);
                        break;
                    case "SHA256":
                        Destination = HashHelper.GetSHA256(Source);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Destination = "Error!\n" + e.Message;
            }
        });
    }
}
