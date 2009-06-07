Feature: Logging out
  Once I am finished with my session
  As a user
  I would like to be able to logout
  
  Scenario: User is logged in and logs out
    Given I am logged in as michael with password password
    When I browse to to the logout page
    Then I should be taken to the home page
    And the site should acknowledge that I am not logged in
    And I should see the login link
    
  Scenario: User is not logged in and logs out
    Given I am not logged in
    When I browse to to the logout page
    Then I should be taken to the home page
    And the site should acknowledge that I am not logged in
    And I should see the login link