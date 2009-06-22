Given /^I have entered (.*) into the calculator$/ do |number|
  puts "entered #{number}"
end

When /^I press add$/ do
  puts "pressed add"
end

Then /^the result should be 120 on the screen$/ do
  puts "result should be 120"
end