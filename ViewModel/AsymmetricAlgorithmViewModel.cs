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
            EncrytData = RSAHelper.EncrytPub(PublicKey, DecrypData);
        }
        catch (Exception e)
        {
            EncrytData = "Error!\n" + e.Message;
        }
    });



    public ICommand EncrytByPrivateKey => new RelayCommand(() =>
    {
        try
        {
            EncrytData = RSAHelper.EncrytPriBatch(PrivateKey, DecrypData);
        }
        catch (Exception e)
        {
            EncrytData = "Error!\n" + e.Message;
        }
    });



    public ICommand DecrypByPublicKey => new RelayCommand(() =>
    {
        try
        {
            DecrypData = RSAHelper.DecrypPubBatch(PublicKey, EncrytData);
        }
        catch (Exception e)
        {
            DecrypData = "Error!\n" + e.Message;
        }
    });


    public ICommand DecrypByPrivateKey => new RelayCommand(() =>
    {
        try
        {
            DecrypData = RSAHelper.DecrypPri(PrivateKey, EncrytData);
        }
        catch (Exception e)
        {
            DecrypData = "Error!\n" + e.Message;
        }
    });

    public ICommand CreateKeyByPriKey => new RelayCommand(() =>
    {
        try
        {
            PublicKey = RSAHelper.CreatePubKeyByPriKey(PrivateKey);
        }
        catch (Exception e)
        {
            DecrypData = "Error!\n" + e.Message;
        }
    });
    
}
