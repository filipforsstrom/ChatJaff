const baseUrl = "http://localhost:5172/";


describe("Login", () => {
  before(function () {
    cy.visit(baseUrl);
    cy.get('[data-cy="reglink"]').click();

    cy.get("#register-email").type("member2000@gmail.com");
    cy.get("#register-username").type("sandy");
    cy.get("#register-password").type("Member123!");
    cy.get("#register-confirmed-password").type("Member123!");
    cy.get("#agree-checkbox").click();
    cy.get("#register-button").click();
  });

  it("logs in user successfully", function () {
    cy.visit(`${baseUrl}account/login`);
    cy.get("#email").type("member2000@gmail.com");
    cy.get("#password").type("Member123!");
    cy.get("#login-button").click();
    cy.contains("Logout");
  });

  it("fails when not a registered member", function () {
    cy.visit(`${baseUrl}account/login`);
    cy.get("#email").type("member300@gmail.com");
    cy.get("#password").type("member2");
    cy.get("#login-button").click();
    cy.contains("Invalid Credentials");
  });

  it("fails when password is wrong", function () {
    cy.visit(`${baseUrl}account/login`);
    cy.get("#email").type("member2000@gmail.com");
    cy.get("#password").type("member2");
    cy.get("#login-button").click();
    cy.contains("Invalid Credentials");
  });

  it("locks out a user after 4 attempts", function () {
    cy.visit(baseUrl);
    cy.get('[data-cy="reglink"]').click();

    cy.get("#register-email").type("member391023@gmail.com");
    cy.get("#register-username").type("megan");
    cy.get("#register-password").type("Member123!");
    cy.get("#register-confirmed-password").type("Member123!");
    cy.get("#agree-checkbox").click();
    cy.get("#register-button").click();

    cy.get("#email").type("member391023@gmail.com");
    cy.get("#password").type("member2");



    for (let i = 0; i < 4; i++) {
      cy.get("#login-button").click().then((btn) => cy.wait(1000))

    }

    cy.contains("User is locked out")
  });

});
