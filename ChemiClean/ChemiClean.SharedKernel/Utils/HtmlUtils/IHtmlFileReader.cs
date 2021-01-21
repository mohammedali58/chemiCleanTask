namespace ChemiClean.SharedKernel

{
    public interface IHtmlFileReader
    {
        string ForgetPasswordHtml(string _culture, string fileName);

        string NewRegistrationHtml(string _culture, string fileName);
    }
}