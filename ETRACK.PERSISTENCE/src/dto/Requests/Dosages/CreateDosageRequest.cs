namespace src.dto.request.dosages;
public record CreateDosageRequest(
    string UserID,
    string Concentration,
    decimal Amount,
    string Ester
);