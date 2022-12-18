namespace LDST.Contracts.Playground;

public record CreatePlaygroundResponse(
    string Name, 
    string Description, 
    string Sport,
    string Address1,
    string Address2,
    string Country,
    string City,
    string? State,
    string ZipCode,
    double AverageRating,
    Uri? TitlePhoto, 
    List<Uri> Photos);