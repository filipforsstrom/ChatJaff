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
    cy.contains('Settings').click();
  });

  it("remove member", () => {
    cy.contains('Remove').click();
    cy.contains('catwoman').should('not.exist');
  })

  it("delete chat", () => {
    cy.contains("Delete Chat").click();
    cy.contains('test chat').should('not.exist');
    cy.contains('My Chat Rooms')
  })

  it("can update a chat name", () => {
    let newChatName = "Chat xyz"
    cy.get('[data-cy="chatname-input"]').type(newChatName);
    cy.get('[data-cy="chatname-updatebtn"]').click()
    cy.get('[data-cy="chatroom-settings-title"]').contains(newChatName)
  })

  it("can't update chat name if below 3 characters", () => {
    let originalChatName;
    cy.get('[data-cy="chatroom-settings-title"]')
      .should('be.visible')
      .then(($value) => {
        originalChatName = $value.text()

        let newChatName = "Ch"
        cy.get('[data-cy="chatname-input"]').type(newChatName);
        cy.get('[data-cy="chatname-updatebtn"]').click()
        cy.contains("The field Name must be a string or array type with a minimum length of '3'.")

        cy.get('[data-cy="chatroom-settings-title"]')
          .should('be.visible')
          .contains(originalChatName)
      })

  })
})