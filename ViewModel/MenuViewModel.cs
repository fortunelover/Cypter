﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Cypter.Model;

namespace Cypter.ViewModel;
public class MenuViewModel: ObservableObject, IRecipient<string>
{
    public static MenuViewModel Current
    {
        get;
        set;
    }

    public MenuViewModel()
    {
        SelectPageCommand = new RelayCommand<object>(selectPageCommand);
        WeakReferenceMessenger.Default.Register(this);
    }

    public ICommand SelectPageCommand { get; set; }

    
    public void selectPageCommand(object obj)
    {
        WeakReferenceMessenger.Default.Send(Convert.ToString(obj));
    }


    public void Receive(string message)
    {
        Current =this;
    }
}
