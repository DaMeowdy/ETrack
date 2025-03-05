using Microsoft.EntityFrameworkCore;
using src.dto.request.dosages;
using src.dto.response.dosages;
using src.exceptions;
using src.models;
using src.services.interfaces;

namespace src.services.concrete;

public sealed class DosageService : IDosageService
{
    private readonly RailwayContext _context;
    public DosageService()
    {
        _context = new RailwayContext();
    }
    public async Task<CreateDosageResponse> CreateDosage(CreateDosageRequest createDosageRequest)
    {
        bool isDosageIDValid = false;
        string _generatedDosageID = await this.GenerateDosageID();
        while (!isDosageIDValid)
        {
            if (await this.NoDuplicateIDS(_generatedDosageID))
            {
                isDosageIDValid = true;
            } else 
            {
                _generatedDosageID = await this.GenerateDosageID();
            }
        }
        try {
            if(! await this.ValidateEster(createDosageRequest))
                throw new InvalidEsterException($"ESTER : [ {createDosageRequest.Ester} ] is not a valid Ester");
            User _User = await _context.Users.SingleOrDefaultAsync<User?>(_usr => _usr.Login.LoginId == createDosageRequest.UserID);
            if (_User is null)
                return new CreateDosageResponse(400, "USER NOT FOUND", null);
            
            Dosage dosage = new Dosage {
                DosageId = _generatedDosageID,
                UserId = _User.UserId,
                Concentration = createDosageRequest.Concentration,
                Ester = createDosageRequest.Ester,
                Amount = createDosageRequest.Amount,
            };
            await this._context.Dosages.AddAsync(dosage);
            await this._context.SaveChangesAsync();
            return new CreateDosageResponse(201,"Created Dosage Object",dosage.ToDTO());
        } catch (InvalidEsterException ex)
        {
            return new CreateDosageResponse(400,$"{ex.Message}", null);
        }
        return null;
    }

    public Task<string> DeleteDosage(string DosageID)
    {
        throw new NotImplementedException();
    }

    public Task<DosageDTO> EditDosage(EditDosageRequest editDosageRequest)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GenerateDosageID()
    {
        string dosageGuidPart = Guid.NewGuid().ToString();
        return await Task.FromResult($"DOSAGE:{dosageGuidPart}");
    }

    public async Task<DosageDTO> GetDosageByDosageID(string DosageID)
    {

        Dosage _dosage = await this._context.Dosages.SingleOrDefaultAsync<Dosage>(dsge => dsge.DosageId == DosageID);
        if (_dosage is null)
            return null;
        else 
            return _dosage.ToDTO();
    }

    public async Task<bool> NoDuplicateIDS(string GenerateDosageID)
    {
        if(! await _context.Dosages.AnyAsync<Dosage>(dosage => dosage.DosageId == GenerateDosageID))
        {
            return await Task.FromResult(true);
        }
        return await Task.FromResult(false);
    }

    public async Task<bool> ValidateEster(CreateDosageRequest createDosageRequest)
    {
        string[] validEsters = [
            "Estradiol Benzoate (in oil)",
            "Estradiol Valerate (in oil)",
            "Estradiol Cypionate (in oil)",
            "Estradiol Cypionate (suspension)",
            "Estradiol Enanthate (in oil)",
            "Estradiol Undecylate (in oil)",
            "Polyestradiol Phosphate"
        ];
        if (validEsters.Any(ester => ester == createDosageRequest.Ester))
            return await Task.FromResult(true);
        else 
            return await Task.FromResult(false);
    }
}