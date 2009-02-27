require 'rake'
require 'rake/tasklib'

module Rake

  class MonoTask < TaskLib
    attr_accessor :name, :projects
	EnvMSBuildBinPath = "MSBuildBinPath"
	EnvMSBuildToolsPath = "MSBuildToolsPath"
	EnvMonoPath = "MONO_PATH"

    VERS_TMPL = { 
		#:release => '!mono! .\tools\mono\gmcs.exe -d:TRACE -debug- -target:!target! -optimize+ -out:!output! -reference:!references! !files!'
		:xbuild=> '"!mono!" "!xbuild!" !project!'
	}

    # Create an mono build task.
	# @param target 	Specifies the format of the output assembly (short: -t) 
	#               	KIND can be one of: exe, winexe, library, module
    def initialize(name=:xbuild) # :yield: self
      @name = name
	  @projects = nil
      yield self if block_given?
      define
    end
    
    # Create the tasks defined by this task lib.
    def define
	  task name do
		# getting the environment variables
		@MSBuildBinPath = ENV[EnvMSBuildBinPath]
		@MSBuildToolsPath = ENV[EnvMSBuildToolsPath]
		@MonoPath = ENV[EnvMonoPath]
		
		# checking the environment variables
		fail RuntimeError, "Environment variable '" + EnvMSBuildBinPath + "' not specified." if @MSBuildBinPath == nil
		fail RuntimeError, "Environment variable '" + EnvMSBuildToolsPath + "' not specified." if @MSBuildToolsPath == nil 
		fail RuntimeError, "Environment variable '" + EnvMonoPath + "' not specified." if @MonoPath == nil 
		
		@MonoPath = @MonoPath + "\\bin\\mono.exe"
		fail RuntimeError, "Unable to find " + @MonoPath + " using environment variable '" + EnvMonoPath + "'." if !File.exist?(@MonoPath)
		fail RuntimeError, "Unable to find " + @MSBuildToolsPath + "\\xbuild.exe" + " using environment variable '" + EnvMSBuildToolsPath + "'." if !File.exist?(@MSBuildToolsPath + "\\xbuild.exe")

        cmd = VERS_TMPL[:xbuild].gsub("!mono!", @MonoPath).gsub("!xbuild!", @MSBuildToolsPath + "\\xbuild.exe")

		fail RuntimeError, "No projects specified" if @projects == nil

		currentDir = Dir.pwd
		@projects.each do |project|
			Dir.chdir(File.dirname(project))
			print "Changed to " + Dir.pwd
			replacedCmd = cmd.gsub("!project!", File.basename(project))
			print "\n\n=====\n===== RAKE: " + project + "\n=====\n\n" + replacedCmd + "\n\n"
			sh replacedCmd
		end
		
		Dir.chdir(currentDir)
      end
      self
    end
  end
end
