const baseUrl = "http://localhost:5172/";

describe("As a member", () => {
  beforeEach(function () {
    cy.visit(baseUrl);
    cy.contains("Login").click();
    cy.get("#email").type("member2@gmail.com");
    cy.get("#password").type("member2");
    cy.get("#login-button").click();
  });
  it("I can enter chatrooms I have access to", function () {
    // Go to chatrooms
    cy.contains("Chat Rooms").click();
    // Enter allowed chatroom
    cy.contains("Chat 1").click();
    // Check if send message button is present
    cy.contains("Send");
  });
  it("I can't enter chatrooms I don't have access to", function () {
    // Go to chatrooms
    cy.contains("Chat Rooms").click();
    // Enter chatroom you're not allowed in
    cy.contains("Chat 2").click();
    // Get redirected
    cy.get('[name="testingName"]').should("be.visible");
  });
});
