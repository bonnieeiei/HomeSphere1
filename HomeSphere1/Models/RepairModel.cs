using System;
using Google.Cloud.Firestore;

namespace HomeSphere1.Models;

public class RepairModel
{
    [FirestoreProperty]
    public string RoomNo {get;set;}

    [FirestoreProperty]
    public string Option {get ; set;}


    [FirestoreProperty]
    public string Detail {get; set;}

}
