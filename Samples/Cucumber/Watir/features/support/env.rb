require 'watir'
require 'spec'

browser = Watir::Browser.new()
#browser.speed = :zippy 

base_url = 'http://localhost:49607'

page_urls = { 
    "login" => "#{base_url}/login",
    "home" => "#{base_url}/home" }

Before do
  @browser = browser
  @page_urls = page_urls
end

at_exit do
  browser.close
end