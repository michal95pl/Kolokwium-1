using Microsoft.AspNetCore.Mvc;
using WebApplication1.Services;

namespace WebApplication1.Controllers;

[ApiController]
[Route("/api/medicaments")]
public class MedicamentController : ControllerBase
{

    private readonly IMedicamentService _medicamentService;

    public MedicamentController(IMedicamentService medicamentService)
    {
        _medicamentService = medicamentService;
    }
    
    [HttpGet]
    public IActionResult GetPrescriptionByMedicament([FromQuery] int idMedicament)
    {
        try
        {
            var data = _medicamentService.GetPrescriptions(idMedicament);
            return Ok(data);
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
        
    }
    
}