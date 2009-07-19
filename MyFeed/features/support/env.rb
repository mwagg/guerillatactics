require 'spec'
require 'watir'

browser = Watir::Browser.new()
browser.bring_to_front
browser.speed = :zippy 

Before do
  @browser = browser
end

at_exit do
  browser.close()
end