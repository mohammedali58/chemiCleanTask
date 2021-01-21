namespace ChemiClean.Core.DTOS
{
    public class BaseDto<TPrimaryKey> where TPrimaryKey : notnull
    {
        public TPrimaryKey Id { get; set; }
    }

    public class BaseDto : BaseDto<int>
    {
    }
}