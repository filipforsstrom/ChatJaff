using ChatJaffApp.UITest.setup;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Xunit.Sdk;

namespace ChatJaffApp.UITest
{
    [Collection(PlaywrightFixture.PlaywrightCollection)]
    public class LoginTest
    {
        private readonly PlaywrightFixture playwrightFixture;
        /// <summary>
        /// Setup test class injecting a playwrightFixture instance.
        /// </summary>
        /// <param name="playwrightFixture">The playwrightFixture
        /// instance.</param>
        public LoginTest(PlaywrightFixture playwrightFixture)
        {
            this.playwrightFixture = playwrightFixture;
        }

        [Fact]
        public async Task MyFirstTest()
        {
            var url = "https://localhost:7085/";
            //// Create the host factory with the App class as parameter and the
            //// url we are going to use.
            //using var hostFactory =
            //  new WebTestingHostFactory<AssemblyClassLocator>();
            //hostFactory
            //  // Override host configuration to mock stuff if required.
            //  .WithWebHostBuilder(builder =>
            //  {
            //      // Setup the url to use.
            //      builder.UseUrls(url);
            //      // Replace or add services if needed.
            //      builder.ConfigureServices(services =>
            //      {
            //          // services.AddTransient<....>();
            //      });
            //      // Replace or add configuration if needed.
            //      builder.ConfigureAppConfiguration((app, conf) =>
            //      {
            //          // conf.AddJsonFile("appsettings.Test.json");
            //      });
            //  })
              // Create the host using the CreateDefaultClient method.
              //.CreateDefaultClient();

            
            // Open a page and run test logic.
            await this.playwrightFixture.GotoPageAsync(
              url,
              async (page) =>
              {
                  // Apply the test logic on the given page.
                  var pageurl = page.Url;
                  pageurl.Should().Be(url);

                  await page.GetByTestId("reglink").ClickAsync();

                  var newUrl = $"{pageurl}account/register";
                  var result = page.Url.Should().Be(newUrl);
              },
              Browser.Chromium);
        }

        [Fact]
        public async Task Login_OnSuccess_NavigatesToHome()
        {
            var url = "https://localhost:7085/";
            // Create the host factory with the App class as parameter and the
            // url we are going to use.
            //using var hostFactory =
            //  new WebTestingHostFactory<AssemblyClassLocator>();
            //hostFactory
            //  // Override host configuration to mock stuff if required.
            //  .WithWebHostBuilder(builder =>
            //  {
            //      // Setup the url to use.
            //      builder.UseUrls(url);
            //      // Replace or add services if needed.
            //      builder.ConfigureServices(services =>
            //      {
            //          // services.AddTransient<....>();
            //      });
            //      // Replace or add configuration if needed.
            //      builder.ConfigureAppConfiguration((app, conf) =>
            //      {
            //          // conf.AddJsonFile("appsettings.Test.json");
            //      });
            //  })
            //  // Create the host using the CreateDefaultClient method.
            //  .CreateDefaultClient();

            var loginUrl = "https://localhost:7085/account/login";

            // Open a page and run test logic.
            await this.playwrightFixture.GotoPageAsync(
              loginUrl,
              async (page) =>
              {
                  // Apply the test logic on the given page.
                  page.Url.Should().Be(loginUrl);

                  await page.GetByTestId("login-input-form")
                    .FillAsync("member2@gmail.com");

                  var loginFieldText = await page.GetByTestId("login-input-form").AllInnerTextsAsync();
                  //loginFieldText.Should().Be("member2@gmail.com");

                  await page.GetByTestId("password-input-form")
                    .FillAsync("member2");

                  await page.GetByTestId("login-button")
                    .ClickAsync();

                  await page.WaitForURLAsync(url);
                  string pageulr = page.Url;
                  page.Url.Should().Be(url);
                
              },
              Browser.Chromium);
        }
    }
}