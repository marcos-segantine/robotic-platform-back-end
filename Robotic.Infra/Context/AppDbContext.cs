using Google.Cloud.Firestore;

namespace Robotic.Infra.Context;

public class AppDbContext
{
    private readonly FirestoreDb _firestoreDb = FirestoreDb.Create();
 
    public CollectionReference GetCollection(string documentId)
    {
        return _firestoreDb.Collection(documentId);
    }
}