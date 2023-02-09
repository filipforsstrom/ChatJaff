const baseUrl = "http://localhost:5172/";

describe("ChatSettings", () => {
    beforeEach(function () {
        cy.visit(baseUrl);
        cy.contains("Login").click();
        cy.get("#email").type("member2@gmail.com");
        cy.get("#password").type("member2");
        cy.get("#login-button").click();
        cy.contains("Logout");
      });

      it("delete chat", () => {
        cy.visit(`${baseUrl}chatrooms/5d728ec3-1f6b-4170-8827-bc064ae25a41`);
        cy.contains("Settings").click();
        cy.contains("Delete Chat").click();
        cy.url().should("eq", baseUrl + "chatrooms");
      })
})