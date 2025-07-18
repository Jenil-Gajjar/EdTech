namespace EdTech.Quiz.Application.DTOs;

public class PaginationDTO
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public string? Filter { get; set; }
    public string? Order { get; set; }
    public bool OrderByDescending { get; set; } = false;
    public int? Count { get; set; }

}
