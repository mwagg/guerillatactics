Given /^I am a registered user$/ do
	@username = 'mike'
end

Given /^I have logged in$/ do
end

Given /^I browse to the "([^\"]*)" page$/ do |page_name|
	@browser.goto(page_url_for page_name)
end

When /^I click the "([^\"]*)" button$/ do |button_text|
	@browser.button(:value, button_text).click
end

Then /^I should see the error "([^\"]*)"$/ do |error_message|
	matching_lis = @browser.ul(:class, 'validation-summary-errors').lis.select { |li| li.innerText == error_message }
	
	if matching_lis.length == 0
		raise "The error \"#{error_message}\" was not found on the page"
	end
end

When /^I type "([^\"]*)" characters into the "([^\"]*)" text field$/ do |no_characters_to_enter, text_field_name|
	text_to_enter = ''
	no_characters_to_enter.to_i.times { text_to_enter += 'a' }
	
	@browser.text_field(:name, text_field_name).set(text_to_enter)
end

When /^I type "([^\"]*)" into the "([^\"]*)" text field$/ do |text, text_field_name|
	@browser.text_field(:name, text_field_name).set(text)
end

Then /^I should be taken to my feed page$/ do
	@browser.url.should == absolute_url("/feed/#{@username}")
end

Then /^I should see a feed update with "([^\"]*)" as the content$/ do |content|
	feed_updates = @browser.divs.select { |div| div.class_name == 'feed-update' }
	matching_feed_updates = feed_updates.select { |div| div.span(:index, 1).innerText == content }
	
	if matching_feed_updates.length == 0
		raise 'No matching feed update was found on the page'
	end
end