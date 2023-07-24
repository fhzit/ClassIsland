﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using GongSolutions.Wpf.DragDrop;

namespace ClassIsland.Models;

public class Profile : ObservableRecipient
{
    private string _name = "";
    private ObservableDictionary<string, TimeLayout> _timeLayouts = new();
    private ObservableDictionary<string, ClassPlan> _classPlans = new();
    private ObservableDictionary<string, Subject> _subjects = new();

    public void NotifyPropertyChanged(string propertyName)
    {
        OnPropertyChanged(propertyName);
    }

    public string Name
    {
        get => _name;
        set
        {
            if (value == _name) return;
            _name = value;
            OnPropertyChanged();
        }
    }

    public ObservableDictionary<string, TimeLayout> TimeLayouts
    {
        get => _timeLayouts;
        set
        {
            if (Equals(value, _timeLayouts)) return;
            _timeLayouts = value;
            OnPropertyChanged();
        }
    }

    public ObservableDictionary<string, ClassPlan> ClassPlans
    {
        get => _classPlans;
        set
        {
            if (Equals(value, _classPlans)) return;
            _classPlans = value;
            OnPropertyChanged();
            _classPlans.CollectionChanged += delegate(object? sender, NotifyCollectionChangedEventArgs args)
            {
                RefreshTimeLayouts();
            };

            RefreshTimeLayouts();
        }
    }

    public void RefreshTimeLayouts()
    {
        foreach (var i in _classPlans)
        {
            i.Value.TimeLayouts = TimeLayouts;
        }
    }

    public ObservableDictionary<string, Subject> Subjects
    {
        get => _subjects;
        set
        {
            if (Equals(value, _subjects)) return;
            _subjects = value;
            OnPropertyChanged();
        }
    }
}