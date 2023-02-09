// const baseUrl = "http://localhost:5172/";

// describe("delete message in chat", () => {
//   beforeEach(function () {
//     cy.visit(baseUrl);
//     cy.contains("Login").click();
//     cy.get("#email").type("member2@gmail.com");
//     cy.get("#password").type("member2");
//     cy.get("#login-button").click();
//     cy.contains("Chat Rooms").click();
//     cy.get("#add-new-chat-button").click();
//     cy.url().should("include", "/chatRooms/createchat");
//     cy.get("#search-user").type("randy");
//     cy.get("#add-chat-member-button").click();
//     cy.get("#chat-name").type("test chat");
//     cy.get("#create-chat-button").click();
//     cy.get("#messageinputarea").type("test text");
//     cy.get("#sendmessagebtn").click();

//   });
//   it("deletes message when pushing button", function () {
//     cy.get("#sendmessagebtn").click();
//     cy.wait(5000);
//     cy.get('[data-cy="delete-message button"]');
//     cy.get('[data-cy="delete-message button"]').click();
//     // cy.get("#sendmessagebtn").click();
//     // cy.get("#delete-message-button").click();
//     // cy.contains("Profile").click();
//     // cy.contains("Chat Rooms").click();
//
//   });

// });
