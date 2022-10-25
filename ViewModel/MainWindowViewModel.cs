using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Cypter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;


namespace Cypter.ViewModel;

//[PropertyChanged.AddINotifyPropertyChangedInterface]
public class MainWindowViewModel : ObservableObject, IRecipient<string>
//public class MainWindowViewModel :ObservableObject
{
    public Page _FrameSource;
    public Page FrameSource
    {
        get => _FrameSource;
        set => SetProperty(ref _FrameSource, value);
    }



    public void Init(int pageId=2)
    {
        switch (pageId)
        {
            case 1: FrameSource = new View.AsymmetricAlgorithmPage(); break;
            case 2: FrameSource = new View.SymmetricAlgorithmPage(); break;
            case 3: FrameSource = new View.HashPage(); break;
            default: FrameSource = new View.SymmetricAlgorithmPage(); break;
        }
    }



    public void Receive(string pID)
    {
        Init(Convert.ToInt32(pID));
    }

    public MainWindowViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
        Init();
    }
}

