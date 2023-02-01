using Microsoft.Playwright.NUnit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatJaffApp.Tests
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    public class Tests : PageTest
    {
        [Test]
        public async Task RegisterUser_ExpectToSeeLoginPage()
        {
            //go to homepage
            await Page.GotoAsync("https://localhost:7085");

            //expect hello world text
            await Expect(Page.GetByText("Hello, world!")).ToBeVisibleAsync();

            //click on login link in nav menu
            await Page.GetByTestId("login-link").ClickAsync();

            //expect to be on login page
            await Expect(Page).ToHaveURLAsync("https://localhost:7085/account/login");

            //click register
            await Page.GetByTestId("register-link").ClickAsync();

            //fill form with user email
            await Page.GetByTestId("register-email").FillAsync("testmember@gmail.com");

            //fill form with username
            await Page.GetByTestId("register-username").FillAsync("testmember");

            //set password
            await Page.GetByTestId("register-password").FillAsync("Test123!");

            //validate password
            await Page.GetByTestId("register-validate-password").FillAsync("Test123!");

            //click register button
            await Page.GetByTestId("register-button").ClickAsync();

            //go to login page
            await Expect(Page).ToHaveURLAsync("https://localhost:7085/account/login");
        }
    }

}
