using System;
using Google.Cloud.Firestore;
using HomeSphere1.Models;
namespace HomeSphere1.Services;

public class FirestoreService
{
    private FirestoreDb db;
    public string StatusMessage;

    public FirestoreService()
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

    public async Task<List<RepairModel>> GetAllRepair()
    {
        try
        {
            await SetupFireStore();
            var data = await db.Collection("Repairs").GetSnapshotAsync();
            var repairs = data.Documents.Select(doc =>
            {
                var repair = new RepairModel();
                repair.RoomNo = doc.GetValue<string>("RoomNo");
                repair.Option = doc.GetValue<string>("Option");
                repair.Detail = doc.GetValue<string>("Detail");
                return repair;
            }).ToList();
            return repairs;
        }
        catch (Exception ex)
        {

            StatusMessage = $"Error: {ex.Message}";
        }
        return null;
    }

    public async Task InsertRepair(RepairModel repair)
    {
        try
        {
            await SetupFireStore();
            var repairData = new Dictionary<string, object>
            {
                { "RoomNo", repair.RoomNo },
                { "Option", repair.Option },
                { "Detail", repair.Detail }
                // Add more fields as needed
            };

            await db.Collection("Repairs").AddAsync(repairData);
        }
        catch (Exception ex)
        {

            StatusMessage = $"Error: {ex.Message}";
        }
    }

    public async Task UpdateRepair(RepairModel repair)
    {
        try
        {
            await SetupFireStore();

            // Manually create a dictionary for the updated data
            var repairData = new Dictionary<string, object>
            {
                { "RoomNo", repair.RoomNo },
                { "Option", repair.Option },
                { "Detail", repair.Detail }
                // Add more fields as needed
            };

            // Reference the document by its Id and update it
            var docRef = db.Collection("Repairs").Document(repair.RoomNo);
            await docRef.SetAsync(repairData, SetOptions.Overwrite);

            StatusMessage = "Repair successfully updated!";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
        }
    }

    internal async Task<IEnumerable<object>> GetAllParcel()
    {
        throw new NotImplementedException();
    }
}
