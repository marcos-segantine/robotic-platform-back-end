using Google.Cloud.Firestore;
using Robotic.Application.DTOs;
using Robotic.Application.Interfaces;
using Robotic.Domain.Entity;
using Robotic.Domain.Enum;
using Robotic.Infra.Context;
using Robotic.Infra.Utils;

namespace Robotic.Infra.Repository;

public class ProfessionalRepository : IProfessionalRepository
{
    private readonly CollectionReference _collectionReference = new AppDbContext().GetCollection("professional");
    
    public async Task Create(Professional professional)
    {
        try
        {
            var documentRef = _collectionReference.Document(professional.Id.ToString());

            var professionalObj = DataUtils.FormatDataToDb(professional);
            
            await documentRef.SetAsync(professionalObj);
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Argument Exception");
            Console.WriteLine(e.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ProfessionalDTO> GetById(Guid id)
    {
        try
        {
            var documentRef = _collectionReference.Document(id.ToString());
            var data = await documentRef.GetSnapshotAsync();

            if (data.Exists == false)
            {
                return null;
            }

            var professional = new ProfessionalDTO (
                data.GetValue<string>("name"),
                data.GetValue<string>("photoPath")
                );
            
            return professional;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Field name not found!");
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Update(Professional professional)
    {
        try
        {
            var documentRef = _collectionReference.Document(professional.Id.ToString());
            
            var professionalObj = DataUtils.FormatDataToDb(professional, new []{"Id"});

            await documentRef.UpdateAsync(professionalObj);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task Delete(Guid id)
    {
        try
        {
            var documentRef = _collectionReference.Document(id.ToString());
            
            await documentRef.DeleteAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<ProfessionalDTO>> GetAll(School? school)
    {
        try
        {
            Query documentsRef;
        
            if (school.HasValue)
            {
                documentsRef = _collectionReference.WhereEqualTo("school", school.ToString());
            }
            else
            {
                documentsRef = _collectionReference;
            }
        
            var data = await documentsRef.GetSnapshotAsync();
            var result = new List<ProfessionalDTO>();

            foreach (var document in data.Documents)
            {
                var newProfessional = new ProfessionalDTO (
                    document.GetValue<string>("name"), 
                    document.GetValue<string>("photoPath")
                );
            
                result.Add(newProfessional);
            }

            return result;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine("Argument Exception");
            Console.WriteLine(e.Message);
            throw;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}