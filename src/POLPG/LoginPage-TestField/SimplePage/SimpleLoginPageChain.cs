namespace TestField
{
    using LoginPage_TestField.Inteface;
    using OpenQA.Selenium;
    
    public class SimpleLoginPageChain : ILoginPageChainned<HomePage>
    {
        private IWebDriver driver;
        private readonly By usernameInputLocator = By.CssSelector("#username");
        
        private readonly By passwordInputLocator = By.CssSelector("#password");
        private readonly By sumitButtonLocator = By.CssSelector("#password");
        
        
        public SimpleLoginPageChain(IWebDriver driver)
        {
            this.driver = driver;
        }


        public ILoginPageChainned<HomePage> TypeLogin(string login)
        {
            driver.FindElement(usernameInputLocator).SendKeys(login);
            return this;
        }

        public ILoginPageChainned<HomePage> TypePassword(string password)
        {
            driver.FindElement(passwordInputLocator).SendKeys(password);
            return this;
        }

        public HomePage ClickLoginButtton()
        {
            driver.FindElement(sumitButtonLocator).Click();
            return new HomePage(this.driver);
        }

        public HomePage Login(string login, string password)
        {
            TypeLogin(login);
            TypePassword(password);
            return ClickLoginButtton();
        }
    }

    public class HomePage
    {
        public HomePage(IWebDriver driver)
        {
            
        }
    }
}


///options to set
/// inheritence
//// is driver comming