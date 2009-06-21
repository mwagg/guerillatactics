require 'spec'
require File.dirname(__FILE__) + '/../../lib/bin/Calculator.dll'

Before do
	@calculator = Calculator::Calculator.new()
end