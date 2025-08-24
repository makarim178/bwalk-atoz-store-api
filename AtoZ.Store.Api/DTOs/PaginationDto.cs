using System;

namespace AtoZ.Store.Api.DTOs;

public class PaginationDto
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;

    public bool IsValid(out List<string> errors)
    {
        errors = [];
        if (Page <= 0)
        {
            errors.Add("Page cannot be zero or less");
        }

        if (PageSize <= 0)
        {
            errors.Add("Page size cannot be zero or less");
        }

        return errors.Count == 0;
    }
}
