const baseUrl = "http://localhost:5172/";

describe("Homepage", () => {
  it("can be visited", () => {
    cy.visit(baseUrl);
  });

  it("front page can be opened", function () {
    cy.visit(baseUrl);
    cy.contains("Hello, world!");
  });
});
