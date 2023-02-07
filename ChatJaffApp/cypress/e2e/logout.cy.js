const baseUrl = "http://localhost:5172/";

describe("logout", () => {
    it("logs out user successfully", function (){
        
        cy.visit(baseUrl);
        //login
        cy.contains("Login").click();
        cy.get("#email").type("member2@gmail.com");
        cy.get("#password").type("member2");
        cy.get("#login-button").click();
        //logout
        cy.contains("Logout").click();
        cy.url().should("include", "account/login");
    })
  

});
