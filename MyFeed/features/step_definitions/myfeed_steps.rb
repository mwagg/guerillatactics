Given /^I browse to the (.*) page$/ do |page_name|
  @browser.goto @pages.getUrlFor(page_name)
end

When /^I enter (.*) into the (.*) text field$/ do |text, text_field_name|
  @browser.text_field(:name, text_field_name).set text
end

When /^I click the (.*) button$/ do |button_value|
  @browser.button(:value, button_value).click
end

Then /^I should be taken to the (.*) page$/ do |page_name|
  @browser.url.should == @pages.getUrlFor(page_name)
end

Then /^the site should acknowledge I am logged in as (.*)$/ do |username|
  @browser.span(:id, 'current-user-info').text.should == 'Currently logged in as ' + username
end

Then /^I should see the logout link$/ do
    @browser.link(:text, 'logout').assert_exists
  end
  
Then /^I should see an error message (.*)$/ do |error_message|
  error_summary = @browser.ul(:class, 'validation-summary-errors')
  error_summary.lis do |li|
    if li.text == error_message then
      return
    end
    
    raise "No error #{error_message} found"
  end
end

Then /^the (.*) text field should be highlighted as failing validation$/ do |text_field_name|
  puts @browser.text_field(:name, text_field_name).class_name.should == 'input-validation-error'
end

Then /^the (.*) text field should be empty$/ do |text_field_name|
  @browser.text_field(:name, text_field_name).value.should == ''
end

Then /^the (.*) text field should contain (.*)$/ do |text_field_name, expected_text|
  @browser.text_field(:name, text_field_name).value.should == expected_text
end

Given /^I am logged in as (.*) with password (.*)$/ do |username, password|
  result = Net::HTTP.post_form(URI.parse(@pages.getUrlFor('login')),
                              {'username'=>username, 'password'=>password})
  result.code.should == "302"
end

When /^I browse to to the logout page$/ do
  @browser.goto(@pages.getUrlFor 'logout')
end

Then /^the site should acknowledge that I am not logged in$/ do
  @browser.span(:id, 'current-user-info').text.should == 'Not logged in'
end

Then /^I should see the login link$/ do
  pending
end

Given /^I am not logged in$/ do
  pending
end