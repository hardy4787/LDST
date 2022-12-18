namespace LDST.Contracts;

public record CreatePlaygroundRequest(
    string Name, 
    string Description, 
    string Sport, 
    string Address1, 
    string Address2,
    string Country, 
    string City,
    string? State, 
    string ZipCode,
    Uri? TitlePhoto, 
    List<Uri> Photos);