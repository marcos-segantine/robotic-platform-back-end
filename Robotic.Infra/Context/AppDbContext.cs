using Google.Cloud.Firestore;
using Microsoft.Extensions.Configuration;

namespace Robotic.Infra.Context;

public class AppDbContext
{
    private readonly FirestoreDb _firestoreDb;

    public AppDbContext()
    {
        try
        {
            var basePath = Path.Combine(Directory.GetParent(Environment.CurrentDirectory).FullName, "Robotic.Infra");
            
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json")
                .Build();
            
            var value = configuration.GetSection("ProjectId").Value;
            
            _firestoreDb = FirestoreDb.Create(value);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
    
    public CollectionReference GetCollection(string documentId)
    {
        return _firestoreDb.Collection(documentId);
    }
}