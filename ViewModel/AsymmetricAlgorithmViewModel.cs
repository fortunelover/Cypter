using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Cypter.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cypter.ViewModel;
public class AsymmetricAlgorithmViewModel : ObservableObject
{
    private string _output;
    public string Output
    {
        get => _output;
        set => SetProperty(ref _output, value);
    }

    private string _input;
    public string Input
    {
        get => _input;
        set => SetProperty(ref _input, value);
    }

    private string _signtext;
    public string SignText
    {
        get => _signtext;
        set => SetProperty(ref _signtext, value);
    }

    private string _errorText;
    public string ErrorText
    {
        get => _errorText;
        set => SetProperty(ref _errorText, value);
    }

    private string _strPri;
    public string PrivateKey
    {
        get => _strPri;
        set => SetProperty(ref _strPri, value);
    }

    private string _strPub;
    public string PublicKey
    {
        get => _strPub;
        set => SetProperty(ref _strPub, value);
    }

    private string _decrypData;
    public string DecrypData
    {
        get => _decrypData;
        set => SetProperty(ref _decrypData, value);
    }

    private string _encrytData;
    public string EncrytData
    {
        get => _encrytData;
        set => SetProperty(ref _encrytData, value);
    }


    public ICommand CreateKey => new RelayCommand(() =>
    {
        RSAHelper.CreateKey(out string pubKey, out string priKey);
        PublicKey = pubKey;
        PrivateKey = priKey;
    });

    public ICommand EncrytByPublicKey => new RelayCommand(() =>
    {
        try
        {
            EncrytData = RSAHelper.EncrytPub(PublicKey, Input);
            Output= string.Empty;
            ErrorText = string.Empty;
        }
        catch (Exception e)
        {
            ErrorText = "Error!\n" + e.Message;
        }
    });



    public ICommand EncrytByPrivateKey => new RelayCommand(() =>
    {
        try
        {
            EncrytData = RSAHelper.EncrytPriBatch(PrivateKey, Input);
            ErrorText = string.Empty;
        }
        catch (Exception e)
        {
            ErrorText = "Error!\n" + e.Message;
        }
    });



    public ICommand DecrypByPublicKey => new RelayCommand(() =>
    {
        try
        {
            DecrypData = RSAHelper.DecrypPubBatch(PublicKey, EncrytData);
            ErrorText = string.Empty;
        }
        catch (Exception e)
        {
            ErrorText = "Error!\n" + e.Message;
        }
    });


    public ICommand DecrypByPrivateKey => new RelayCommand(() =>
    {
        try
        {
            DecrypData = RSAHelper.DecrypPri(PrivateKey, EncrytData);
            ErrorText = string.Empty;
        }
        catch (Exception e)
        {
            ErrorText = "Error!\n" + e.Message;
        }
    });

    public ICommand CreateKeyByPriKey => new RelayCommand(() =>
    {
        try
        {
            PublicKey = RSAHelper.CreatePubKeyByPriKey(PrivateKey);
            ErrorText = string.Empty;
        }
        catch (Exception e)
        {
            ErrorText = "Error!\n" + e.Message;
        }
    });



    public ICommand Java2Net => new RelayCommand(() =>
    {
        try
        {
            PrivateKey = RSAHelper.RSAPrivateKeyJava2DotNet(PrivateKey);
            ErrorText = string.Empty;
        }
        catch (Exception e)
        {
            ErrorText = "Error!\n" + e.Message;
        }
    });

    public ICommand Net2Java => new RelayCommand(() =>
    {
        try
        {
            PrivateKey = RSAHelper.RSAPrivateKeyDotNet2Java(PrivateKey);
            ErrorText = string.Empty;
        }
        catch (Exception e)
        {
            ErrorText = "Error!\n" + e.Message;
        }
    });

    public ICommand SignData => new RelayCommand(() =>
    {
        try
        {
            this.SignText = RSAHelper.SignData(Input, PrivateKey);
            Output = "签名成功";
            ErrorText = string.Empty;
        }
        catch (Exception e)
        {
            ErrorText = "Error!\n" + e.Message;
        }
    });

    public ICommand VerifyData => new RelayCommand(() =>
    {
        try
        {
            Output = RSAHelper.VerifyData(Input, PublicKey,SignText)?"验签成功": "验签失败";
            ErrorText = string.Empty;
        }
        catch (Exception e)
        {
            ErrorText = "Error!\n" + e.Message;
        }
    }); 


}
