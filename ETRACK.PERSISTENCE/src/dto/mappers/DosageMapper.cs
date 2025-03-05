using src.dto.response.dosages;
using src.models;

public static class DosageMapper {
    public static DosageDTO ToDTO(this Dosage dosage) => new DosageDTO(dosage);
}