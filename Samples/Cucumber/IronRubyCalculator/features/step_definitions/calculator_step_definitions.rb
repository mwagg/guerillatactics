Given /^I have entered (.*) into the calculator$/ do |number|
  @calculator.EnterNumber(number.to_i)
end

When /^I press add$/ do
  @calculator.Add()
end

Then /^the result should be (.*) on the screen$/ do |expected_result|
  @calculator.Result().should == expected_result.to_i
end
