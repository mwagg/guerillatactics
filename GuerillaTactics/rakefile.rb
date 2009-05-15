rakefile_dir = File.dirname(__FILE__)

$LOAD_PATH.push File.join(rakefile_dir, "..")

require 'buildsupport/msbuild'
require 'buildsupport/nunit'

NUNIT_CONSOLE_PATH = File.join(rakefile_dir, '../tools/nunit/nunit-console.exe')
BUILD_OUTPUT_DIR = File.join(rakefile_dir, '../build')
SOLUTION_FILE = File.join(rakefile_dir, 'guerillatactics.sln')

task :clean do
	if File.exists?(BUILD_OUTPUT_DIR)
		rm_rf(BUILD_OUTPUT_DIR)
	end
end

Rake::MSBuildTask.new(:build) do |msbuild|
	msbuild.project_file = SOLUTION_FILE
end

Rake::NUnitTask.new(:test) do |nunit|
	nunit.nunit_console_path = NUNIT_CONSOLE_PATH
	nunit.test_assembly_pattern = '**/bin/release/*Tests.dll'
end

task :copy_to_build do
	Dir.mkdir(BUILD_OUTPUT_DIR)
	Dir.glob("**/bin/Release/*.*").each do |file|
		copy(file, BUILD_OUTPUT_DIR)
	end
end

task :default => [:clean, :build, :test, :copy_to_build]