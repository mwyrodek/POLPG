namespace TestField
{
    using LoginPage_TestField.Inteface;
    using OpenQA.Selenium;
    
    public class SimpleLoginPage : ILoginPage
    {
        private IWebDriver driver;
        private readonly By usernameInputLocator = By.CssSelector("#username");
        
        private readonly By passwordInputLocator = By.CssSelector("#password");
        private readonly By sumitButtonLocator = By.CssSelector("#password");
        
        
        public SimpleLoginPage(IWebDriver driver)
        {
            this.driver = driver;
        }


        public void TypeLogin(string login)
        {
            driver.FindElement(usernameInputLocator).SendKeys(login);
        }

        public void TypePassword(string password)
        {
            driver.FindElement(passwordInputLocator).SendKeys(password);
        }

        public void ClickLoginButtton()
        {
            driver.FindElement(sumitButtonLocator).Click();
        }

        public void Login(string login, string password)
        {
            TypeLogin(login);
            TypePassword(password);
            ClickLoginButtton();
        }
    }
}


///options to set
/// inheritence
//// is driver comming