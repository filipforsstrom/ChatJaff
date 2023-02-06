const baseUrl = "http://localhost:5172/";

describe("wait to make sure that everything is loaded", () => {
  it("can be visited", () => {
    cy.wait(5000);
    cy.visit(baseUrl);
  });
});
