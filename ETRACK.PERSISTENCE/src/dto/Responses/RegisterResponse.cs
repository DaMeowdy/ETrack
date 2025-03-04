namespace src.dto.response;
record RegisterResponse (
    int code,
    string JWT,
    string recoveryPhrase
);