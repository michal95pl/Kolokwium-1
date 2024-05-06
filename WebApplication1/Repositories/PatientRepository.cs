using System.Data.SqlClient;

namespace WebApplication1.Repositories;

public interface IPatientRepository
{
    public bool DeletePatient(int idPatient);
    public bool PatientExist(int idPatient);
}

public class PatientRepository : IPatientRepository
{

    private readonly IConfiguration _configuration;

    public PatientRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public bool DeletePatient(int idPatient)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();
        
        using var transaction = connection.BeginTransaction();

        try
        {
            using var command = new SqlCommand("Delete Patient WHERE IdPatient = @idPatient", connection);
            command.Transaction = transaction;
            command.Parameters.AddWithValue("@idPatient", idPatient);
            command.ExecuteNonQuery();

            command.CommandText = @"Delete Prescription WHERE IdPatient = @idPatient";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@idPatient", idPatient);
            command.ExecuteNonQuery();
            
            transaction.Commit();

            return true;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }

    public bool PatientExist(int idPatient)
    {
        using var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        connection.Open();

        var query = "SELECT IdPatient FROM Patient WHERE IdPatient = @idPatient";
        using var command = new SqlCommand(query, connection);
        command.Parameters.AddWithValue("@idPatient", idPatient);
        var id = command.ExecuteScalar();

        if (id is null)
            return false;
        
        return true;
    }
}