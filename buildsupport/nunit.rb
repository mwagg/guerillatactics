#!/usr/bin/env ruby

require 'rake'
require 'rake/tasklib'

module Rake
	class NUnitTask < TaskLib
		attr_accessor :name, :nunit_console_path
		
		def initialize(name = 'nunit')
			@name = name
			@nunit_console_path = 'nunit.exe'
			
			yield self if block_given?
			define
		end
		
		def addTestAssembly(assembly)
			@assemblies = "#{@assemblies} #{assembly}"
		end
		
		def define
			task name do
				sh "#{@nunit_console_path}#{@assemblies}"
			end
		end
	end
end