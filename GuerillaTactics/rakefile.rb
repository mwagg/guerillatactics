$LOAD_PATH.push File.join(File.dirname(__FILE__), "..")

require 'buildsupport/msbuild'

BUILD_OUTPUT_DIR = '../build'
SOLUTION_FILE = File.join(File.dirname(__FILE__), 'guerillatactics.sln')

task :clean do
	if File.exists?(BUILD_OUTPUT_DIR)
		rm_rf(BUILD_OUTPUT_DIR)
	end
end

Rake::MSBuildTask.new(:build_guerillatactics) do |msbuild|
	msbuild.project_file = SOLUTION_FILE
end

task :copy_to_build do
	Dir.mkdir(BUILD_OUTPUT_DIR)
	Dir.glob("**/bin/Release/*.*").each do |file|
		copy(file, BUILD_OUTPUT_DIR)
	end
end

task :default => [:clean, :build_guerillatactics, :copy_to_build]