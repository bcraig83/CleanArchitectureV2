namespace CleanArchitecture.Application
{
    public class ApplicationOptions
    {
        public bool StoreAuthorInLowercase { get; set; } = false;

        public override string ToString()
        {
            return "{{" +
                $"{nameof(StoreAuthorInLowercase)}={StoreAuthorInLowercase}" +
                "}}";
        }
    }
}