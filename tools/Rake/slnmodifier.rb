require 'rake'

class SlnModifier
	attr_reader :source
	
	def initialize(sourceFileName)
		@source = sourceFileName
	end

	def create(framework)
    target = File.new((@source.gsub /sln$/, "#{framework}.g.sln"), "w")
    sln_fix = @source.gsub /sln$/, "#{framework}.rb"

    if File.exist? sln_fix then
      require sln_fix
      fix target, framework
    else
      puts "Using default SlnModifier"
      source = File.new(@source)
      source.each_line { |line| target.write line.gsub(/\.csproj/, ".#{framework}.g.csproj") }
      source.close
      target.close
    end
	end
end