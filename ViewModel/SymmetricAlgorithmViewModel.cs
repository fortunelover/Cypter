using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Cypter.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Cypter.ViewModel
{
    public class SymmetricAlgorithmViewModel : ObservableObject
    {
        private string _ciphertext;
        public string Ciphertext
        {
            get => _ciphertext;
            set => SetProperty(ref _ciphertext, value);
        }

        private string _plaintext;
        public string Plaintext
        {
            get => _plaintext;
            set => SetProperty(ref _plaintext, value);
        }

        private string _key;
        public string Key
        {
            get => _key;
            set => SetProperty(ref _key, value);
        }

        private string _iv;
        public string IV
        {
            get => _iv;
            set => SetProperty(ref _iv, value);
        }


        private CipherMode GetEnumeratorCipherMode(string cipherModeStr)
        {
            switch (cipherModeStr)
            {
                case "CBC":
                    return CipherMode.CBC;
                case "OFB":
                    return CipherMode.OFB;
                case "CFB":
                    return CipherMode.CFB;
                case "CTS":
                    return CipherMode.CTS;
                case "ECB":
                    return CipherMode.ECB;
                default:
                    return CipherMode.ECB;
            }
        }
        private PaddingMode GetEnumeratorPaddingMode(string paddingModeStr)
        {
            switch (paddingModeStr)
            {
                case "None":
                    return PaddingMode.None;
                case "Zeros":
                    return PaddingMode.Zeros;
                case "ISO10126":
                    return PaddingMode.ISO10126;
                case "ANSIX923":
                    return PaddingMode.ANSIX923;
                case "PKCS7":
                    return PaddingMode.PKCS7;
                default:
                    return PaddingMode.PKCS7;
            }
        }
        public ICommand Encryt => new RelayCommand(() =>
        {
            try
            {
                CipherMode cipherMode = GetEnumeratorCipherMode(CipherModeStr);
                PaddingMode paddingMode = GetEnumeratorPaddingMode(PaddingModeStr);
                Ciphertext = AESHelper.AesEncrypt(Plaintext, Key, cipherMode, paddingMode, EncordingModeStr, IV);
            }
            catch (Exception e)
            {
                Ciphertext = "Error!\n" + e.Message;
            }
        });
        public ICommand Decryp => new RelayCommand(() =>
        {
            try
            {
                CipherMode cipherMode = GetEnumeratorCipherMode(CipherModeStr);
                PaddingMode paddingMode = GetEnumeratorPaddingMode(PaddingModeStr);
                Plaintext = AESHelper.AesDecrypt(Ciphertext, Key, cipherMode, paddingMode, EncordingModeStr, IV);
            }
            catch (Exception e)
            {
                Plaintext = "Error!\n" + e.Message;
            }
        });

        public ICommand CreateKey => new RelayCommand(() =>
        {
            try
            {
                AESHelper.AesCreateKey(out string key, out string iv, EncordingModeStr);
                Key = key;
                IV = iv;
            }
            catch (Exception e)
            {
                Key = "Error!\n" + e.Message;
            }
        });

        private string _cipherMode;
        public string CipherModeStr
        {
            get => _cipherMode;
            set => SetProperty(ref _cipherMode, value);
        }

        private string _paddingMode;
        public string PaddingModeStr
        {
            get => _paddingMode;
            set => SetProperty(ref _paddingMode, value);
        }

        private string _encordingModeStr;
        public string EncordingModeStr
        {
            get => _encordingModeStr;
            set => SetProperty(ref _encordingModeStr, value);
        }
    }
}
