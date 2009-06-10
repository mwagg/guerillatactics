rakefile_dir = File.dirname(__FILE__)

$LOAD_PATH.push File.join(rakefile_dir, "..")

require 'cucumber'
require 'cucumber/rake/task'
require 'buildsupport/msbuild'
require 'buildsupport/nunit'

NUNIT_CONSOLE_PATH = File.join(rakefile_dir, '../tools/nunit/nunit-console.exe')
BUILD_OUTPUT_DIR = File.join(rakefile_dir, '../build')
SOLUTION_FILE = File.join(rakefile_dir, 'myfeed.sln')


Rake::MSBuildTask.new(:build) do |msbuild|
	msbuild.project_file = SOLUTION_FILE
end

namespace :tests do
  Rake::NUnitTask.new(:unit) do |nunit|
    nunit.nunit_console_path = NUNIT_CONSOLE_PATH
    nunit.test_assembly_pattern = '**/bin/release/*Tests.dll'
  end
  
  Cucumber::Rake::Task.new(:acceptance) do |t| 
    t.cucumber_opts = "--color --no-source"
  end
end

task :default => [:build, "tests:unit"]