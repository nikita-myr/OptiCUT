using System;
using Avalonia.Controls;
using ReactiveUI;

namespace OptiCUT.ViewModels;

public class SettingsViewModel: ViewModelBase
{
    private string? _saveDir;
    private string _cutFileName = "CutResult";
    private string _specificationFileName = "SpecificationResult";
    private bool _generatePdf;

    private string _objectLabel = "Объект";
    private string _constructionLabel = "В-1";

    public string? SaveDir
    {
        get => _saveDir; 
        set => this.RaiseAndSetIfChanged(ref _saveDir, value);
    }

    public string CutFileName
    {
        get => _cutFileName;
        set => this.RaiseAndSetIfChanged(ref _cutFileName, value);
    }

    public string SpecificationFileName
    {
        get => _specificationFileName;
        set => this.RaiseAndSetIfChanged(ref _specificationFileName, value);
    }

    public string ObjectLabel
    {
        get => _objectLabel;
        set => this.RaiseAndSetIfChanged(ref _objectLabel, value);
    }

    public string ConstructionLabel
    {
        get => _constructionLabel;
        set => this.RaiseAndSetIfChanged(ref _constructionLabel, value);
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
        if (SaveDir != "") Models.Excel.ExcelGenerator.GenerateCutterExcelFile(SaveDir, CutFileName, makePdf:GeneratePdf);
        
        //TODO: Show warning "Empty save path!" 
    }

    private void GenerateSpecificationExcel()
    {
        if(SaveDir != "") Models.Excel.ExcelGenerator.GenerateSpecificationFile(SaveDir , SpecificationFileName, 
            objectLabel: ObjectLabel, constructionLabel: ConstructionLabel ,makePdf: GeneratePdf);
    }

    private async void SelectSaveDirectoryButton()
    {
        var openFolderDialog = new OpenFolderDialog();
        var result = await openFolderDialog.ShowAsync(new Window());
        if (result != null)
        {
            SaveDir = result + "/";
        }
    }
    
    
    
}