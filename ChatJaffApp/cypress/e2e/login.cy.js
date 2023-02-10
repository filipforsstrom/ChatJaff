const baseUrl = "http://localhost:5172/";

describe("Login", () => {
  beforeEach(function () {
    cy.visit(baseUrl);
    cy.get('[data-cy="reglink"]').click();

    cy.get("#register-email").type("member3@gmail.com");
    cy.get("#register-username").type("sandy");
    cy.get("#register-password").type("Member123!");
    cy.get("#register-confirmed-password").type("Member123!");
    cy.get("#register-button").click();
  });

  it("logs in user successfully", function () {
    cy.get("#email").type("member3@gmail.com");
    cy.get("#password").type("Member123!");
    cy.get("#login-button").click();
    cy.contains("Logout");
  });

  it("fails when not a registered member", function () {
    cy.get("#email").type("member300@gmail.com");
    cy.get("#password").type("member2");
    cy.get("#login-button").click();
    cy.contains("Invalid Credentials");
  });

  it("fails when password is wrong", function () {
    cy.get("#email").type("member3@gmail.com");
    cy.get("#password").type("member2");
    cy.get("#login-button").click();
    cy.contains("Invalid Credentials");
  });

  it("locks out a user after 4 attempts", function () {
    cy.get("#email").type("member3@gmail.com");
    cy.get("#password").type("member2");

    var loginBtn = cy.get("#login-button");
    loginBtn.click();
    cy.contains("Invalid Credentials");

    for (let i = 0; i < 3; i++) {
      loginBtn.click()
    }

    cy.contains("User is locked out")
  });
});
