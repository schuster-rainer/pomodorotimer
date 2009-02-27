class Settings
	attr_accessor :BuildDir, :user, :password, :data_source, :catalog
  
	def to_s
		result = ""
		self.instance_variables.sort.each do |var|
			result += var + "=" + self.instance_variable_get(var) + "\n"
		end
		
		result
	end
end