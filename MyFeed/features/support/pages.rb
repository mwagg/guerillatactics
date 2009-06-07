class Pages
  def initialize()
    @base_url = 'http://localhost:49607/'
    
    @pages = {}
    
    @pages['home'] = @base_url
    @pages['login'] = @base_url + 'login/'
    @pages['logout'] = @base_url + 'logout/'
  end
  
  def getUrlFor(page_name)
    @pages[page_name]
  end
end