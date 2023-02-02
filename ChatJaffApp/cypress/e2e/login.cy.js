const baseUrl = 'http://localhost:5172/'

describe('Homepage', () => {
  it('can be visited', () => {
    setTimeout(() => {

    }, 10000);
    cy.visit(baseUrl)
  })

  it('can navigate to register page', () => {
    cy.visit(`${baseUrl}account/register`)
  })

  // it('can navigate to about page', () => {
  //   cy.visit(`${baseUrl}about`)
  // })

  it('can kill the application', () => {
    cy.request('delete', `${baseUrl}api/identity/kill`,)
  })
})