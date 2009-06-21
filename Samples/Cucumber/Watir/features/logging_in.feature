Feature: Logging in
  In order to do something interesting
  As a user
  I want to be able to log in to the site
  
  Scenario: Logging in with correct credentials
    Given I am viewing the "login" page
    When I type "bob" into the "username" text field
    And I type "password" into the "password" text field
    And I click the "Login" button
    Then I should be taken to the "home" page
    And the page should say "Logged in"
    
  Scenario: Logging in with incorrect password
    Given I am viewing the "login" page
    When I type "bob" into the "username" text field
    And I type "the wrong password" into the "password" text field
    And I click the "Login" button
    Then I should remain on the "login" page
  
  Scenario: Logging in with an unknown username
    Given I am viewing the "login" page
    When I type "phil" into the "username" text field
    And I type "password" into the "password" text field
    And I click the "Login" button
    Then I should remain on the "login" page  