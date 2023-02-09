const baseUrl = "http://localhost:5172/";

describe("ChatSettings", () => {
    beforeEach(function () {
        cy.visit(baseUrl);
        cy.contains("Login").click();
        cy.get("#email").type("member2@gmail.com");
        cy.get("#password").type("member2");
        cy.get("#login-button").click();

      cy.contains("Chat Rooms").click();
      cy.contains("Create new chat");
      cy.get("#add-new-chat-button").click();
      cy.url().should("include", "/chatRooms/createchat");
      cy.get("#add-chat-member-button").click();
      cy.contains("Search field must not be empty.");
      cy.get("#search-user").type("catwoman");
      cy.get("#add-chat-member-button").click();
      cy.get("#chat-name").type("test chat");
      cy.get("#create-chat-button").click();
      });

      it("delete chat", () => {
        cy.contains("Settings").click();
        cy.contains("Delete Chat").click();
        cy.contains('test chat').should('not.exist');
        cy.contains('My Chat Rooms').should('exist');
      })
})