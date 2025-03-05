using src.dto.response.dosages;

public record CreateDosageResponse(
    int status,
    string statusDescription,
    DosageDTO data
);