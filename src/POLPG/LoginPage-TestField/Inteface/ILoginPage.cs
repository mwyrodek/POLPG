namespace LoginPage_TestField.Inteface
{
    public interface ILoginPage
    {

        void TypeLogin(string login);

        void TypePassword(string password);


        void ClickLoginButtton();

        void Login(string login, string password);

    }
}