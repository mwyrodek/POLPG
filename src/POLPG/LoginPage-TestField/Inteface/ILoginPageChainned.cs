namespace LoginPage_TestField.Inteface
{
    public interface ILoginPageChainned<T>
    {
        ILoginPageChainned<T> TypeLogin(string login);

        ILoginPageChainned<T> TypePassword(string password);


        T ClickLoginButtton();

        T Login(string login, string password);


    }
}