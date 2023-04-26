using System;
using Avalonia.Controls;
using ReactiveUI;

namespace OptiCUT.ViewModels;

public class SettingsViewModel: ViewModelBase
{
    private string? _saveDir;
    private string _fileName = "CutResult";
    private bool _generatePdf;

    public string? SaveDir
    {
        get => _saveDir; 
        set => this.RaiseAndSetIfChanged(ref _saveDir, value);
    }

    public string FileName
    {
        get => _fileName;
        set => this.RaiseAndSetIfChanged(ref _fileName, value);
    }

    public bool GeneratePdf
    {
        get => _generatePdf;
        set => this.RaiseAndSetIfChanged(ref _generatePdf, value);
    }
    
    
    public SettingsViewModel()
    {
    }

    private void GenerateCutExcel()
    {
        Console.WriteLine(SaveDir);
        if (SaveDir != "") Excel.ExcelGenerator.GenerateExcelFile(SaveDir, FileName, makePdf: GeneratePdf);
        
        //TODO: Show warning "Empty save path!" 
    }

    private async void SelectSaveDirectoryButton()
    {
        var openFolderDialog = new OpenFolderDialog();
        var result = await openFolderDialog.ShowAsync(new Window());
        if (result != null)
        {
            SaveDir = result + "/";
            Console.WriteLine(result);
        }
    }
    
    
    
}