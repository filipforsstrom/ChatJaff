const baseUrl = "http://localhost:5172/";

describe("Kill app", function () {
  it("front page can be opened", function () {
    cy.visit(baseUrl);
  });

  after(() => {
    fetch(`${baseUrl}api/identity/kill`, {
      method: "DELETE",
    });
  });
});
