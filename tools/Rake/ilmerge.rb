require 'rake'
require 'rake/tasklib'

module Rake
  class ILMerge < TaskLib
    attr_accessor :name, :output_file, :log_file, :target_kind, 
                :version, :xmldocs, :input_assemblies, :toolpath, 
                :allow_wildcards, :input_directories
    
    def initialize(name=:ilmerge)
      @name = name
      @output_file = nil
      @log_file = nil
      @target_kind = nil # winexe|library|exe
      @version = nil
      @xmldocs = nil
      @input_assemblies = nil
      @toolpath = nil
      @allow_wildcards = false
      @input_directories = nil
      yield self if block_given?
      define
    end
        
    def define
        check_preconditions
        execute_command
    end
    
    private
    def check_preconditions
        raise 'An output_file is required to merge files' if @output_file.nil?
        raise 'input_assemblies are required to merge' if @input_assemblies.nil?
        raise 'Toolpath for ILMerge is required' if @toolpath.nil?
    end
    
    def execute_command           
        task @name do
            #sh "#{toolpath.escape} /wildcards /out:#{output_file.escape} #{input_assemblies.join(" ")}"
            merge_command = get_merge_command
            puts "executin ILMerge with command ..."            
            puts merge_command
            sh merge_command
        end
    end
    
    def eval_string( property, option )
        if property
            "/#{option.escape}:#{property.escape} "
        end
        ""
    end
    
    def get_merge_command
        "#{toolpath.escape} #{eval_string(target_kind,"target")} #{eval_string(version, "ver")} #{eval_string(xmldocs, "xmldocs")} #{eval_string(log_file, "log")} #{eval_flag(allow_wildcards, "wildcards")} /out:#{output_file.escape} #{input_assemblies.join(" ")}"
    end
    
    def eval_flag( flag, option)
         if allow_wildcards 
            "/wildcards" 
         else
            ""
         end
    end
  end
end


