const baseUrl = "http://localhost:5172/";

describe("Dashboard", () => {
  beforeEach(function () {
    cy.visit(baseUrl);
    cy.contains("Login").click();
    cy.get("#email").type("admin1@gmail.com");
    cy.get("#password").type("admin1");
    cy.get("#login-button").click();
    // cy.contains("Logout");
  });

  it("ban toBan1", () => {
    cy.contains("Dashboard").click();
    cy.contains("tr", "toBan1").find("button").click();
    cy.contains("toBan1 banned successfully");
  });

  it("Find member", () => {
    cy.contains("Dashboard").click();
    cy.get("#searchbar").click().type("randy");
  });
});
