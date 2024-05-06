using WebApplication1.Model;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public interface IMedicamentService
{
    List<PrescriptionModel> GetPrescriptions(int idMedicament);
}

public class MedicamentService : IMedicamentService
{

    private readonly IMedicamentRepositiory _medicamentRepositiory;

    public MedicamentService(IMedicamentRepositiory medicamentRepositiory)
    {
        _medicamentRepositiory = medicamentRepositiory;
    }
    
    public List<PrescriptionModel> GetPrescriptions(int idMedicament)
    {
        var medicamentPrescriptions = _medicamentRepositiory.getAllMedicamentPrescriptions(idMedicament);

        if (medicamentPrescriptions.Count == 0)
            throw new Exception("prescriptions for medicament not found");

        return medicamentPrescriptions;

    }
}