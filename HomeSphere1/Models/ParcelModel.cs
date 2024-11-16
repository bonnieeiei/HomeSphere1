using System;
using Google.Cloud.Firestore;

namespace HomeSphere1.Models;

public class ParcelModel
{
    [FirestoreProperty]
    public string RoomNo {get;set;}

    [FirestoreProperty]
    public string Date {get ; set;}


    [FirestoreProperty]
    public string ParcelNo {get; set;}
}
