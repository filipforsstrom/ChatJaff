const baseUrl = 'http://localhost:5172/'

describe('Homepage', () => {

  it('can be visited', () => {
    cy.wait(10000)
    cy.visit(baseUrl)
  })

  it('can navigate to register page', () => {
    cy.visit(`${baseUrl}account/register`)
  })

  it('can navigate to about page', () => {
    cy.visit(`${baseUrl}about`)
  })

  // it('can kill the application', () => {
  //   cy.request({
  //     method: 'DELETE',
  //     url: `${baseUrl}api/identity/kill`,
  //     failOnStatusCode: false,
  //     failsOnNetworkCode: false
  //   })
  // })

  after(() => {
    fetch(`${baseUrl}api/identity/kill`, {
      method: 'DELETE'
    })
  })
})