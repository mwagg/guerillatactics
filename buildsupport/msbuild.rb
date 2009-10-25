#!/usr/bin/env ruby

require 'rake'
require 'rake/tasklib'

module Rake
	class MSBuildTask < TaskLib
		attr_accessor :name, :project_file, :configuration
	
		def initialize(name = :msbuild) 
			@name = name
			@configuration = 'Release'
			
			yield self if block_given?
			define
		end
		
		def define
			task name do
				sh "#{msbuild_path} #{@project_file} /p:Configuration=#{@configuration}"
			end
		end

		def msbuild_path
			"#{ENV['WINDIR']}\\Microsoft.Net\\Framework\\v3.5\\msbuild.exe"
		end
	end
end