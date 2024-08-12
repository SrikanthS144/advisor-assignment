namespace Domain.Extend
{
    public interface IAuditable
    {
        int CreatedBy { get; set; }
        DateTime Created { get; set; }
        int UpdatedBy { get; set; }
        DateTime Updated { get; set; }
    }
}
