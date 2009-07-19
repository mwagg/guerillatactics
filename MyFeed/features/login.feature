Feature: Logging in
  In order to identify myself to my_feed
  As a user
  I want to be able to login to my_feed
  
  Background:
	Given I am a registered user with username "michael" and password "password"
	and I browse to the login page
  
  Scenario: Logging in with correct credentials
    When I type "michael" into the "Username" text field
    And I type "password" into the "Password" text field
    And I click the Login button
    Then I should be taken to the "home" page
    And the site should acknowledge I am logged in as michael
    And I should see the logout link
  
  Scenario: Logging in with incorrect credentials
    When I type" michael" into the "Username" text field
    And I enter "the_wrong_password" into the "Password" text field
    And I click the "Login" button
    Then I should see the error "Username or password incorrect. Please try again"
    
  Scenario: Attempting to login without a username or password
    When I click the Login button
    Then I should see the error "Please enter your username"
    And I should see the error "Please enter your password"
    And the "Username" text field should be highlighted as failing validation
    And the "Password" text field should be highlighted as failing validation
    And the "Username" text field should be empty
    
  Scenario: Attempting to login without a password
    When I type "michael" into the "Username" text field
    And I click the "Login" button
    Then I should see the error "Please enter your password"
    And the "Username" text field should contain "michael"
	And the "Password" text field should be highlighted as failing validation
    And the "Password" text field should be empty
    
  Scenario: Attempting to login without a username
    And I type "password" into the "Password" text field
    When I click the "Login" button
    Then I should see the error "Please enter your username"
    And the "Username" text field should be empty
    And the "Password" text field should be empty
  