const baseUrl = "http://localhost:5172/";
describe("test", function () {
  // beforeEach(function () {
  //   cy.visit("https://localhost:7085/");
  // });

  it("front page can be opened", function () {
    cy.visit(baseUrl);
    cy.contains("Welcome to your new app.");
    cy.contains("Hello, world!");
  });

  //   after(() => {
  //     fetch(`${baseUrl}api/identity/kill`, {
  //       method: "DELETE",
  //     });
  //   });
});
