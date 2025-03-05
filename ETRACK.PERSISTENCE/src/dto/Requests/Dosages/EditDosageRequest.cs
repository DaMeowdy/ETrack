using System.Reflection.Metadata;

namespace src.dto.request.dosages;
public record EditDosageRequest(
    string DosageID,
    string UserID,
    string Concentration,
    decimal Amount,
    string Ester
);
