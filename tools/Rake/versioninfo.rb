require 'rake'
require 'rake/tasklib'
require 'erb'

module Rake
	class AssemblyInfo < TaskLib
		attr_accessor :name, :file, :attributes
		
		def initialize(name=:assemblyinfo)
			@name = name
			@attributes = {}
			yield self if block_given?
			define
		end
		
		def define
			raise "The file to generate must be given" if @file.nil
			raise "The version to write to version info file must be given" if @version.nil
			
			desc "Generates a file containing the version info of the project"
			task @name do
			
				write(@file)
			
			end
			end
			
			self
		end
		
		def write(file)
		template = %q{
			using System;
			using System.Reflection;
			using System.Runtime.InteropServices;

			<% @properties.each {|key, value| %>
				[assembly: <% = key %>("<% = value %>")]
			<% } %>
		}.gsub(/^\w+/, '')
		  
		erb = ERB.new(template, 0, "%<>")
	  
		File.open(file, 'w') do |file|
			file.puts erb.result(binding) 
		end
	end
end