const baseUrl = "http://localhost:5172/";

describe("Chatroom", () => {
  it("can get stored chat messages", function () {
    cy.visit(baseUrl);
    cy.contains("Login").click();
    cy.get("#email").type("member2@gmail.com");
    cy.get("#password").type("member2");
    cy.get("#login-button").click();
    cy.contains("Logout");
    cy.visit(`${baseUrl}chatrooms/5d728ec3-1f6b-4170-8827-bc064ae25a41`);
    cy.contains("Cathy, Meet me at the batcave girrl");
  });
});
