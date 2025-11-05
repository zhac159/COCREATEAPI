namespace Application.Features.Common.Records;

public abstract record ProjectRecordBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required DateTime Date { get; set; }
    public required LocationRecord Location { get; set; }
}

public record ProjectRecord : ProjectRecordBase
{
    public required int Id { get; set; }
}

public record CreateProjectRecord : ProjectRecordBase { }

public record UpdateProjectRecord : ProjectRecordBase
{
    public required int Id { get; set; }
}
