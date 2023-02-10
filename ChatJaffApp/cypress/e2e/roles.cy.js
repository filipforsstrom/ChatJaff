const baseUrl = "http://localhost:5172/";

describe("Dashboard", () => {
  it("goto dashboard as admin", () => {
    cy.visit(baseUrl);
    cy.contains("Login").click();
    cy.get("#email").type("admin1@gmail.com");
    cy.get("#password").type("admin1");
    cy.get("#login-button").click();
    cy.contains("Dashboard").click();
    cy.contains("Members");
  });

  it("goto dashboard as member", () => {
    cy.visit(baseUrl);
    cy.contains("Login").click();
    cy.get("#email").type("member1@gmail.com");
    cy.get("#password").type("member1");
    cy.get("#login-button").click();
    cy.contains("Logout");
    cy.visit(baseUrl + "dashboard");
    cy.contains("Please login to view this content.");
  });
});
