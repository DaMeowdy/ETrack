using src.models;
namespace src.dto.response.dosages;
public sealed class DosageDTO{
    public string Concentration { get; init;}
    public decimal Amount { get; init; }
    public string Ester {get; init;}
    public DosageDTO(Dosage _dosage)
    {
        this.Concentration = _dosage.Concentration;
        this.Amount = _dosage.Amount;
        this.Ester = _dosage.Ester;
    }
}
