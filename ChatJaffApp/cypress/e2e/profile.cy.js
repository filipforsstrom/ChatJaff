const baseUrl = "http://localhost:5172/";

describe("Profile", () => {
  beforeEach(function () {
    cy.visit(baseUrl);
    cy.contains("Login").click();
    cy.get("#email").type("member2@gmail.com");
    cy.get("#password").type("member2");
    cy.get("#login-button").click();
    cy.contains("Profile").click();
  });

  it("shows profile visibility", function () {
    cy.contains("Profile");
  });

  it("fails to change username when already exists", function () {
    cy.contains("Change Username")
    cy.get("#new-username-field").type("member1")
    cy.get("#change-username-button").click();
    cy.contains("Failed to change username.")
  })

  it("change username", function () {
    cy.get("#new-username-field").type("member3")
    cy.get("#change-username-button").click()
    cy.contains("Username change success!")
  })

  it("fails when changing password", function () {
    //change password without confirming new password
    cy.get("#old-password-field").type("member2");
    cy.get("#change-password-button").click();
    cy.contains("The NewPassword field is required");

    //change password with wrong amount of characters
    cy.get("#old-password-field").type("member2");
    cy.get("#new-password-field").type("member3");
    cy.get("#change-password-button").click();
    cy.get("#change-password-button").click();
    cy.contains("Failed");
  });

  it("successfully changes password", function () {
    cy.get("#old-password-field").type("member2");
    cy.get("#new-password-field").type("Member123!");
    cy.get("#change-password-button").click();
    cy.contains("Password changed");
  });

  //change username to name that already exists
  
  //change username successfully
  
});

describe("delete account", () => {
  beforeEach(function () {
    cy.visit(baseUrl);
    cy.contains("Login").click();
    cy.get("#email").type("member2@gmail.com");
    cy.get("#password").type("Member123!");
    cy.get("#login-button").click();
    cy.contains("Profile").click();
    cy.contains("Profile");
  });

  it("Deletes the account", function () {
    //try delete account
    cy.get("#delete-account-button").click();
    cy.contains("Really delete profile?");

    //cancel delete
    cy.get("#cancel-delete-button").click();

    //delete account
    cy.get("#delete-account-button").click();
    cy.contains("Really delete profile?");

    //confirm delete
    cy.get("#confirm-delete-button").click();
    cy.url().should("include", "/account/login");
  });
});
