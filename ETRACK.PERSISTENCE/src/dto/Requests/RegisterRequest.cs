namespace src.dto.request;
record RegisterRequest (
    string username, 
    string email, 
    string password
);