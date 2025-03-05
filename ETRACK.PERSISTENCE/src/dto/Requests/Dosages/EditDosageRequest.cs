using System.Reflection.Metadata;

namespace src.dto.request.dosages;
public record EditDosageRequest(
    string UserID,
    string Concentration,
    decimal Amount,
    string Ester
);
