using System.Data.SqlClient;
using WebApplication1.Model;
using WebApplication1.Services;

namespace WebApplication1.Repositories;

public interface IMedicamentRepositiory
{
   List<PrescriptionModel> getAllMedicamentPrescriptions(int idMedicament);
}

public class MedicamentRepositiory : IMedicamentRepositiory
{

   private readonly IConfiguration _configuration;

   public MedicamentRepositiory(IConfiguration configuration)
   {
      _configuration = configuration;
   }

   public List<PrescriptionModel> getAllMedicamentPrescriptions(int idMedicament)
   {
      using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
      connection.Open();
      
      using var command = new SqlCommand("SELECT P.IdPrescription, Date, DueDate, IdPatient, IdDoctor FROM Prescription_Medicament "+
      "INNER JOIN Prescription P on Prescription_Medicament.IdPrescription = P.IdPrescription "+
      "WHERE IdMedicament = @idMedicament " + 
      "ORDER BY Date DESC;", connection);

      command.Parameters.AddWithValue("@idMedicament", idMedicament);

      using var reader = command.ExecuteReader();
      
      var prescriptions = new List<PrescriptionModel>();

      while (reader.Read())
      {
         var prescription = new PrescriptionModel()
         {
            IdPrescription = (int)reader["IdPrescription"],
            Date = (DateTime)reader["Date"],
            DueDate = (DateTime)reader["DueDate"],
            IdPatient = (int)reader["IdPatient"],
            IdDoctor = (int)reader["IdDoctor"]
         };
         prescriptions.Add(prescription);
      }

      return prescriptions;
   }
}