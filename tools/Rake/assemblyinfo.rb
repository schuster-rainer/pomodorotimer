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
			raise "The file to generate must be given" if @file.nil?
			
			desc "Generates a file containing the assembly information (typically AssemblyInfo.cs)"
			task @name do
				write(@file)
			end
			
			self
		end
		
		def write(file)
			template = %q{
				using System;
				using System.Reflection;
				using System.Runtime.InteropServices;

				<% @attributes.each do |key, value| %>
					[assembly: <%= key %>("<%= value %>")]
				<% end %>
			}.gsub(/^\s+/, '')

			erb = ERB.new(template, 0, "%<>")
 
			File.open(file, 'w') do |f|
				f.puts erb.result(binding)
			end
			
			puts "Created file #{file}"
		end
	end
end