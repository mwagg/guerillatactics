module Pages
  def initialize_pages()
    @base_url = 'http://localhost:49607'
    
    @pages = {}
    
    @pages['home'] = ''
    @pages['login'] = '/login'
    @pages['logout'] = '/logout'
	@pages['post a feed update'] = '/feed/post'
  end
  
  def pages(page_name)
	initialize_pages if @pages == nil 
	@pages[page_name]
  end
  
  def absolute_url(relative_url)
	@base_url + relative_url
  end
  
  def page_url_for(page_name)
	raise "Cannot find url for page #{page_name}." if pages(page_name) == nil
    absolute_url(pages(page_name))
  end
end

World(Pages)