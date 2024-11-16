using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using HomeSphere1.Models;
using HomeSphere1.Services;
using PropertyChanged;

namespace HomeSphere1.ViewModels;

[AddINotifyPropertyChangedInterface]
public class ParcelViewModel
{
    FirestoreService _firestoreService;
    public ObservableCollection<ParcelModel> Parcels { get; set; } = [];
    public ParcelModel CurrentParcel { get; set; }

    public ICommand All { get; set; }
    public ICommand WaitingCommand { get; set; }
    public ICommand ConfirmCommand { get; set; }

    public ParcelViewModel(FirestoreService firestoreService)
    {
        this._firestoreService = firestoreService;
        this.Refresh();
        All = new Command(async () =>
        {
            CurrentParcel = new ParcelModel();
            await this.Refresh();
        }
        );
        WaitingCommand = new Command(async () =>
        {
            await this.Refresh();
        });
        ConfirmCommand = new Command(async () =>
        {
            await this.Refresh();
        });
    }

    public async Task GetAll()
    {
        Parcels = [];
        var items = await _firestoreService.GetAllParcel();
        foreach (var item in items)
        {
            Parcel.Add(item);
        }
    }

    private async Task Refresh()
    {
        CurrentParcel = new ParcelModel();
        await this.GetAll();
    }

}
