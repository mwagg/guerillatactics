require 'spec'
require 'watir'

browser = Watir::Browser.new()
browser.bring_to_front
browser.speed = :zippy 

Before do
  @pages = Pages.new
  @browser = browser
end

at_exit do
  browser.close()
end