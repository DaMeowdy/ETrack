using src.dto.request.dosages;
using src.dto.response.dosages;

namespace src.services.interfaces;

public interface IDosageService
{
    public Task<CreateDosageResponse> CreateDosage(CreateDosageRequest createDosageRequest);
    public Task<string> DeleteDosage(string DosageID);
    public Task<DosageDTO> EditDosage(EditDosageRequest editDosageRequest);
    public Task<DosageDTO> GetDosageByDosageID(string DosageID);
    Task<bool> ValidateEster(CreateDosageRequest createDosageRequest);
    Task<bool> NoDuplicateIDS(string GenerateDosageID);
    Task<string> GenerateDosageID();
}