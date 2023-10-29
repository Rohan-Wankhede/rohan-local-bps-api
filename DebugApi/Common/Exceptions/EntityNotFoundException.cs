namespace DebugApi.Common.Exceptions;

public class EntityNotFoundException : CommonException
{
    public EntityNotFoundException(string entityName, object? id = null)
    : base(nameof(EntityNotFoundException), BuildErrorMessage(entityName, id))
    {
    }

    private static string BuildErrorMessage(string entityName, object? id)
    {
        return id == null ? $"{entityName} was not found." : $"{entityName} with ID ({id}) was not found!";
    }
}
