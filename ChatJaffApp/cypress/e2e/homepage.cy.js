const baseUrl = "http://localhost:5172/";

describe("Homepage", () => {
  it("can be visited", () => {
    cy.wait(10000);
    cy.visit(baseUrl);
  })
})
