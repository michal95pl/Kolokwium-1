using System.Data;
using WebApplication1.Repositories;

namespace WebApplication1.Services;

public interface IPatientService
{
    void DeletePatient(int idPatient);
}

public class PatientService : IPatientService
{

    private readonly IPatientRepository _patientRepository;
    
    public PatientService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
    
    public void DeletePatient(int idPatient)
    {
        if (!_patientRepository.PatientExist(idPatient))
            throw new Exception("Patient not exists");

        if (!_patientRepository.DeletePatient(idPatient))
            throw new DataException();
    }
}