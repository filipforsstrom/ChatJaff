const baseUrl = "http://localhost:5172/";

describe("create chat", () => {
  beforeEach(function () {
    cy.visit(baseUrl);
    cy.contains("Login").click();
    cy.get("#email").type("member2@gmail.com");
    cy.get("#password").type("member2");
    cy.get("#login-button").click();
    cy.contains("Chat Rooms").click();
  });
  it("creates a new chat", function () {
      //creates a chatroom
      cy.contains("Chat Rooms").click();
      cy.contains("Create new chat");
      cy.get("#add-new-chat-button").click();
      //add members to chat
      cy.url().should("include", "/chatRooms/createchat");
      cy.get("#add-chat-member-button").click();
      cy.contains("Search field must not be empty.");
      cy.get("#search-user").type("catwoman");
      cy.get("#add-chat-member-button").click();
      cy.contains("Remove");
      //set chat name
      cy.get("#chat-name").type("test chat");
      //choose encryption
      cy.get("#encrypted-chat-checkbox").click();
      //create
      cy.get("#create-chat-button").click();
      //in chatroom
      cy.contains("Send");
  })

})