require '../buildsupport/msbuild.rb'

BUILD_OUTPUT_DIR = '../build'

task :clean do
	if File.exists?(BUILD_OUTPUT_DIR)
		rm_rf(BUILD_OUTPUT_DIR)
	end
end

Rake::MSBuildTask.new(:build_guerillatactics) do |msbuild|
	msbuild.project_file = 'guerillatactics.sln'
end

task :copy_to_build do
	Dir.mkdir(BUILD_OUTPUT_DIR)
	Dir.glob("**/bin/Release/*.*").each do |file|
		copy(file, BUILD_OUTPUT_DIR)
	end
end

task :default => [:clean, :build_guerillatactics, :copy_to_build]