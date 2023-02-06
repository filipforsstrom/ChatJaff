const baseUrl = "http://localhost:5172/";
describe("Homepage", () => {
  it("can be visited", () => {
    cy.visit(baseUrl);
    cy.contains("Hello, world!");
  });
});