using System.Data;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("/api/patient")]
public class PatientController : ControllerBase
{

    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }
    
    [HttpDelete]
    public IActionResult DeletePatient([FromQuery] int idPatient)
    {
        try
        {
            _patientService.DeletePatient(idPatient);
        }
        catch (DataException e)
        {
            return Conflict();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        
        return NoContent();
    }
    
}