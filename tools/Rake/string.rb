class String
	def escape
		"\"#{self.to_s}\""
	end
	
	def in(dir)
		File.join(dir, self)
	end
end