require 'rake'
require "rexml/document"

REXML::Attribute.class_eval( %q^
  def to_string
    %Q[#@expanded_name="#{to_s().gsub(/"/, '&quot;')}"]
  end
  ^ )

REXML::XMLDecl.class_eval( %q^
  private
      def content(enc)
        rv = "version=\"#@version\""
        rv << " encoding=\"#{enc}\"" if @writeencoding || enc !~ /UTF-8/i
        rv << " standalone=\"#@standalone\"" if @standalone
        rv
      end
  ^)

class CSProjModifier
  attr_reader :source
	
  def initialize(sourceFileName)
    @source = sourceFileName
  end

  def create(framework)
    target = @source.gsub /csproj$/, "#{framework}.g.csproj"
    csproj_fix = @source.gsub /csproj$/, "#{framework}.rb"

	file = File.new(@source)
	doc = REXML::Document.new file

    # puts doc.elements["Project/ItemGroup/Reference"].parent

    if File.exist? csproj_fix then
      require csproj_fix
      fix doc
      output = File.new(target, "w")
      doc.write output
      output.close
    else
      msg = "WARNING: Missing #{csproj_fix} to create .csproj file for #{framework} framework. Ignoring #{@source}.";
      puts msg
      TeamCity::append_build_status_text(msg)
    end
    
	end

end