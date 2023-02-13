const baseUrl = "http://localhost:5172/";

describe("register", () => {
  beforeEach(function () {
    cy.visit(baseUrl);
    cy.get('[data-cy="reglink"]').click();
  });

  it("Registers a new user", function () {
    cy.get("#register-email").type("member4@gmail.com");
    cy.get("#register-username").type("sandy4");
    cy.get("#register-password").type("Member123!");
    cy.get("#register-confirmed-password").type("Member123!");
    cy.get("#agree-checkbox").click();
    cy.get("#register-button").click();
    cy.url().should("include", "account/login");
  });
});
