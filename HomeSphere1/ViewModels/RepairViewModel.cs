using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using HomeSphere1.Models;
using HomeSphere1.Services;
using PropertyChanged;

namespace HomeSphere1.ViewModels;

[AddINotifyPropertyChangedInterface]
public class RepairViewModel
{
    FirestoreService _firestoreService;

    public ObservableCollection<RepairModel> Repairs { get; set; } = [];
    public RepairModel CurrentRepair { get; set; }

    public ICommand Done { get; set; }

    public RepairViewModel(FirestoreService firestoreService)
    {
        this._firestoreService = firestoreService;
        this.Refresh();
        Done = new Command(async () =>
        {
            await this.Save();
            await this.Refresh();
        });
    }

    public async Task GetAll()
    {
        Repairs = [];
        var items = await _firestoreService.GetAllRepair();
        foreach (var item in items)
        {
            Repairs.Add(item);
        }
    }

    public async Task Save()
    {
        if (string.IsNullOrEmpty(CurrentRepair.RoomNo))
        {
            await _firestoreService.InsertRepair(this.CurrentRepair);
        }
        else
        {
            await _firestoreService.UpdateRepair(this.CurrentRepair);
        }
    }

    private async Task Refresh()
    {
        CurrentRepair = new RepairModel();
        await this.GetAll();
    }

}
