const baseUrl = 'http://localhost:5172/'

describe('Homepage', () => {
  it('can be visited', () => {
    setTimeout(() => {
      cy.visit(baseUrl)
    }, 20000);
  })

  it('can navigate to register page', () => {
    cy.visit(`${baseUrl}account/register`)
  })

  // it('can navigate to about page', () => {
  //   cy.visit(`${baseUrl}about`)
  // })

  it('can kill the application', () => {
    cy.request({
      method: 'DELETE',
      url: `${baseUrl}api/identity/kill`,
      failOnStatusCode: false
    })
    // cy.request('delete', `${baseUrl}api/identity/kill`, failOnStatusCode: true)
  })
})