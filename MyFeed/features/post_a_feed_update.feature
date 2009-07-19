Feature: Posting an update to a feed
	In order to share what I am doing
	Ad a registered user
	I would like to post an update to my feed
	
	Background:
		Given I am a registered user
		And I have logged in
		And I browse to the "post a feed update" page
		
	Scenario: User tries to post an empty update
		When I click the "Post" button
		Then I should see the error "Please enter your update"
		
	Scenario: User tries to post an update over 140 characters
		When I type "141" characters into the "Content" text field
		And I click the "Post" button
		Then I should see the error "Your update must be 140 characters or less"
		
	Scenario: User enters a valid message
		When I type "This is my feed update" into the "Content" text field
		And I click the "Post" button
		Then I should be taken to my feed page
		And I should see a feed update with "This is my feed update" as the content