Given /^I am viewing the "([^\"]*)" page$/ do |page|
  @browser.goto(@page_urls[page])
end

When /^I type "([^\"]*)" into the "([^\"]*)" text field$/ do |text, text_field_name|
  @browser.text_field(:name, text_field_name).set(text)
end

When /^I click the "([^\"]*)" button$/ do |button_value|
  @browser.button(:value, button_value).click
end

Then /^I should be taken to the "([^\"]*)" page$/ do |expected_page|
  @browser.url.should == @page_urls[expected_page]
end

Then /^the page should say "([^\"]*)"$/ do |expected_text|
  @browser.text.should include(expected_text)
end

Then /^I should remain on the "([^\"]*)" page$/ do |expected_page|
  Then "I should be taken to the \"#{expected_page}\" page"
end
