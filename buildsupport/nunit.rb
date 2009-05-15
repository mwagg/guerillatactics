#!/usr/bin/env ruby

require 'rake'
require 'rake/tasklib'

module Rake
	class NUnitTask < TaskLib
		attr_accessor :name, :nunit_console_path, :test_assembly_pattern
		
		def initialize(name = 'nunit')
			@name = name
			@nunit_console_path = 'nunit.exe'
			@test_assembly_pattern = '**/*Tests.dll'
			
			yield self if block_given?
			define
		end
		
		def define
			task name do
				assemblies = ''
				Dir.glob(@test_assembly_pattern).each do |test_assembly|
					assemblies = "#{assemblies} #{test_assembly}"
				end
			
				sh "#{@nunit_console_path}#{assemblies}"
			end
		end
	end
end