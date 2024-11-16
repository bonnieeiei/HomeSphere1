using System;
using Google.Cloud.Firestore;
using HomeSphere1.Models;

namespace HomeSphere1.Services;

public class FirestoreServiceP
{
    private FirestoreDb db;
    public string StatusMessage;

    public FirestoreServiceP()
    {
        this.SetupFireStore();
    }

    private async Task SetupFireStore()
    {
        if (db == null)
        {
            var stream = await FileSystem.OpenAppPackageFileAsync("homesphere-d3847-firebase-adminsdk-jygc8-cb4b680929.json");
            var reader = new StreamReader(stream);
            var contents = reader.ReadToEnd();
            db = new FirestoreDbBuilder
            {
                ProjectId = "homesphere-d3847",

                JsonCredentials = contents
            }.Build();
        }

    }

    public async Task<List<ParcelModel>> GetAllParcel()
    {
        try
        {
            await SetupFireStore();
            var data = await db.Collection("Parcels").GetSnapshotAsync();
            var parcels = data.Documents.Select(doc =>
            {
                var parcel = new ParcelModel();
                parcel.RoomNo = doc.GetValue<string>("RoomNo");
                parcel.Date = doc.GetValue<string>("Date");
                parcel.ParcelNo = doc.GetValue<string>("ParcelNo");
                return parcel;
            }).ToList();
            return parcels;
        }
        catch (Exception ex)
        {

            StatusMessage = $"Error: {ex.Message}";
        }
        return null;
    }

    public async Task InsertParcel(ParcelModel parcel)
    {
        try
        {
            await SetupFireStore();
            var parcelData = new Dictionary<string, object>
            {
                { "RoomNo", parcel.RoomNo },
                { "Date", parcel.Date },
                { "ParcelNo", parcel.ParcelNo }
                // Add more fields as needed
            };

            await db.Collection("s").AddAsync(parcelData);
        }
        catch (Exception ex)
        {

            StatusMessage = $"Error: {ex.Message}";
        }
    }

    public async Task UpdateParcel(ParcelModel parcel)
    {
        try
        {
            await SetupFireStore();

            // Manually create a dictionary for the updated data
            var parcelData = new Dictionary<string, object>
            {
                { "RoomNo", parcel.RoomNo },
                { "Date", parcel.Date },
                { "ParcelNo", parcel.ParcelNo }
                // Add more fields as needed
            };

            // Reference the document by its Id and update it
            var docRef = db.Collection("Parcels").Document(parcel.RoomNo);
            await docRef.SetAsync(parcelData, SetOptions.Overwrite);

            StatusMessage = "Parcel successfully updated!";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
    }
}
