const baseUrl = "http://localhost:5172/";

describe("Dashboard", () => {
  beforeEach(function () {
    cy.visit(baseUrl);
    cy.contains("Login").click();
    cy.get("#email").type("member2@gmail.com");
    cy.get("#password").type("member2");
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
