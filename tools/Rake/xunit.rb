require 'rake'
require 'rake/tasklib'

module Rake
	class XUnit < TaskLib
		attr_accessor :name, :assembly, :toolpath
		
		def initialize(name=:xunit)
			@name = name
			yield self if block_given?
			define
		end
		
		def define
			raise 'A test assembly is required to run XUnit' if @assembly.nil?
						
			desc "Runs XUnit tests"
			task @name do
				Dir.glob(@assembly).each do |a|
					sh "#{@toolpath.escape} #{a.escape}" # /html #{File.basename(a)}.html"
				end
			end
			
			self
		end
	end
end