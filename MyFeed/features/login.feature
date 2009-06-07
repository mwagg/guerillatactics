Feature: Logging in
  In order to identify myself to MyFeed
  As a user
  I want to be able to login to MyFeed
  
  Scenario: Logging in with correct credentials
    Given I browse to the login page
    When I enter michael into the Username text field
    And I enter password into the Password text field
    When I click the Login button
    Then I should be taken to the home page
    And the site should acknowledge I am logged in as michael
    And I should see the logout link
  
  Scenario: Logging in with incorrect credentials
    Given I browse to the login page
    When I enter michael into the Username text field
    And I enter the_wrong_password into the Password text field
    When I click the Login button
    Then I should see an error message Username or password incorrect. Please try again
    
  Scenario: Attempting to login without a username or password
    Given I browse to the login page
    When I click the Login button
    Then I should see an error message Please enter your username
    And I should see an error message Please enter your password
    And the Username text field should be highlighted as failing validation
    And the Password text field should be highlighted as failing validation
    And the Username text field should be empty
    
  Scenario: Attempting to login without a password
    Given I browse to the login page
    And I enter michael into the Username text field
    When I click the Login button
    Then I should see an error message Please enter your password
    And the Username text field should contain michael
    And the Password text field should be empty
    
  Scenario: Attempting to login without a username
    Given I browse to the login page
    And I enter password into the Password text field
    When I click the Login button
    Then I should see an error message Please enter your username
    And the Username text field should be empty
    And the Password text field should be empty
  