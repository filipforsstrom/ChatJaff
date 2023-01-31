using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace ChattJaffApp.Tests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    //[Test]
    //public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    //{
    //    int nr = 6;
    //    await Page.GotoAsync("https://playwright.dev");

    //    // Expect a title "to contain" a substring.
    //    await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));

    //    // create a locator
    //    var getStarted = Page.GetByRole(AriaRole.Link, new() { Name = "Get started" });

    //    // Expect an attribute "to be strictly equal" to the value.
    //    await Expect(getStarted).ToHaveAttributeAsync("href", "/docs/intro");

    //    // Click the get started link.
    //    await getStarted.ClickAsync();

    //    // Expects the URL to contain intro.
    //    await Expect(Page).ToHaveURLAsync(new Regex(".*intro"));
    //}

    [Test]
    public async Task LoginUserFromHomePage_ExpektToSeeLougOutButton()
    {
        //go to homepage
        await Page.GotoAsync("https://localhost:7085/");

        //expect hello world text
        await Expect(Page.GetByText("Hello, world!")).ToBeVisibleAsync();

        //click on login link in nav menu
        await Page.GetByTestId("login-link").ClickAsync();

        //expect to be on login page
        await Expect(Page).ToHaveURLAsync("https://localhost:7085/account/login");

        //fill form with user email
        await Page.GetByTestId("login-input-form").FillAsync("member2@gmail.com");

        //fill form with user password
       await Page.GetByTestId("password-input-form").FillAsync("member2");

        //click on login button
        await Page.GetByTestId("login-button").ClickAsync();


        //expect to see logout link in navmenu
        await Expect(Page.GetByTestId("logout-link")).ToBeVisibleAsync();



    }

}