using System;
using System.Collections.ObjectModel;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using OptiCUT.Models;

namespace OptiCUT.ViewModels;

public class WindowsAndVentilationCutterViewModel: ViewModelBase
{
    public static ObservableCollection<Window> Windows { get; set; }
    public static ObservableCollection<Ventilation> Ventilations { get; set; }


    public WindowsAndVentilationCutterViewModel()
    {
        Windows = new ObservableCollection<Window>();
        Ventilations = new ObservableCollection<Ventilation>();
    }

    
    private void AddVentilationButton()
    {
        Ventilations.Add(new Ventilation());
    }

    private void RemoveVentilationButton()
    {
        if (Ventilations.Count > 0) Ventilations.Remove(Ventilations[^1]);
    }

    private void AddWindowButton()
    {
        Windows.Add(new Window("ОК-1", 0,0, 0, " ", " ", false, " ", 5));
    }

    private void RemoveWindowButton()
    {
        if(Windows.Count > 0) Windows.Remove(Windows[^1]);
    }
    
}