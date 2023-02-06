
describe("test", function () {
  // beforeEach(function () {
  //   cy.visit("https://localhost:7085/");
  // });

  it("front page can be opened", function () {
    cy.visit("https://localhost:7085/");
    cy.contains("Welcome to your new app.");
    cy.contains("Hello, world!");
  });

//   after(() => {
//     fetch(`${baseUrl}api/identity/kill`, {
//       method: "DELETE",
//     });
//   });
});
