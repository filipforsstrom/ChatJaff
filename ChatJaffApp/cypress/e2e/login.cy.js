const baseUrl = "http://localhost:5172/";

describe("Login", () => {
beforeEach(function () {
  cy.visit(baseUrl);
  
});
  it("logs in user successfully", function () {
    cy.contains("Login").click();
    cy.get("#email").type("member2@gmail.com");
    cy.get("#password").type("member2");
    cy.get("#login-button").click();
    cy.contains("Logout");
  });

  it("fails login when not a member", function () {
    cy.contains("Login").click();
    cy.get("#email").type("member300@gmail.com");
    cy.get("#password").type("member2");
    cy.get("#login-button").click();
    cy.contains("Login failed");
  });

  
});
