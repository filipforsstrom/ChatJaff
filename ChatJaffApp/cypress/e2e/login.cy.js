const baseUrl = "http://localhost:5172/";

describe("Homepage", () => {
  it("can be visited", () => {
    cy.wait(10000);
    cy.visit(baseUrl);
  });

  it("can navigate to register page", () => {
    cy.visit(`${baseUrl}account/register`);
  });

  it("can navigate to about page", () => {
    cy.visit(`${baseUrl}about`);
  });

  // it('can kill the application', () => {
  //   cy.request({
  //     method: 'DELETE',
  //     url: `${baseUrl}api/identity/kill`,
  //     failOnStatusCode: false,
  //     failsOnNetworkCode: false
  //   })
  // })

  // it("login to page with user", function () {
  //   cy.contains("Login").click();
  //   cy.get("#email").type("member2@gmail.com");
  //   cy.get("#password").type("member2");
  //   cy.get("#login-button").click();
  //   cy.contains("Logout");
  // });

  // it("login fail when not a member", function () {
  //   cy.contains("Login").click();
  //   cy.get("#email").type("member300@gmail.com");
  //   cy.get("#password").type("member2");
  //   cy.get("#login-button").click();
  //   cy.contains("Login failed");
  // });

  // it("profile page can be opened when logged in", function () {
  //   // login
  //   cy.contains("Login").click();
  //   cy.get("#email").type("member2@gmail.com");
  //   cy.get("#password").type("member2");
  //   cy.get("#login-button").click();

  //   cy.contains("Profile").click();
  //   cy.contains("User Info");
  // });

  // it("Create a chat", function () {
  //   // login
  //   cy.contains("Login").click();
  //   cy.get("#email").type("member2@gmail.com");
  //   cy.get("#password").type("member2");
  //   cy.get("#login-button").click();

  //   //creates a chatroom
  //   cy.contains("Chat Rooms").click();
  //   cy.contains("Create new chat");
  //   cy.contains("+").click();

  //   //add members to chat
  //   cy.url().should("include", "/chatRooms/createchat");
  //   cy.get("#add-button").click();
  //   cy.contains("Search field must not be empty.");
  //   cy.get("#search-user").type("catwoman");
  //   cy.get("#add-button").click();
  //   cy.contains("Remove");

  //   //set chat name
  //   cy.get("#chat-name").type("test chat");

  //   //create
  //   cy.get("#create-chat-button").click();

  //   //enter created chat from chat rooms page
  //   cy.contains("Chat Rooms").click();
  //   cy.contains("test chat").click();
  // });

  // it("Log out", function () {
  //   // login
  //   cy.contains("Login").click();
  //   cy.get("#email").type("member2@gmail.com");
  //   cy.get("#password").type("member2");
  //   cy.get("#login-button").click();

  //   //logout
  //   cy.contains("Logout").click();
  //   cy.url().should("include", "/account/login");
  // });

  // it("Delete account", function () {
  //   // login
  //   cy.contains("Login").click();
  //   cy.get("#email").type("member2@gmail.com");
  //   cy.get("#password").type("member2");
  //   cy.get("#login-button").click();

  //   //go to profile
  //   cy.contains("Profile").click();
  //   cy.contains("User Info");

  //   //try delete account
  //   cy.get("#delete-account-button").click();
  //   cy.contains("Really delete profile?");

  //   //cancel delete
  //   cy.get("#cancel-delete-button").click();

  //   //delete account
  //   cy.get("#delete-account-button").click();
  //   cy.contains("Really delete profile?");

  //   //confirm delete
  //   cy.get("#confirm-delete-button").click();
  //   cy.url().should("include", "/account/register");
  // });

  // it("Register new user", function () {
  //   cy.contains("Login").click();
  //   cy.get("#register-link").click();
  //   cy.get("#register-email").type("member3");
  //   cy.get("#register-username").type("sandy");
  //   cy.get("#register-password").type("Member3!");
  //   cy.get("#register-confirmed-password").type("Member3!");
  //   cy.get("#register-button").click();
  //   cy.contains("Hello, world!");
  // });

  // it("Change Password ", function () {
  //   // login
  //   cy.contains("Login").click();
  //   cy.get("#email").type("member2@gmail.com");
  //   cy.get("#password").type("member2");
  //   cy.get("#login-button").click();

  //   //go to profile
  //   cy.contains("Profile").click();
  //   cy.contains("User Info");

  //   //change password without confirming new password
  //   cy.get("#change-password").type("member2");
  //   cy.get("#confirm-password-button").click();
  //   cy.contains("New password is required");

  //   //change password fail with wrong amount of characters
  //   cy.get("#new-password").type("member3");
  //   cy.get("#confirm-password-button").click();
  //   cy.get("#confirm-password-button").click();
  //   cy.contains("Failed");

  //   //change password
  //   cy.get("#new-password").type("Member3!");
  //   cy.get("#confirm-password-button").click();
  //   cy.contains("Password changed.");

  // })
});
