module TeamCity
	def teamcity_progress(task)
		teamcity_service_message "progressStart", task
		yield if block_given?
		teamcity_service_message "progressFinish", task
	end
	
	def teamcity_service_message(type, message)
		puts "##teamcity[#{type} '#{message}']"
	end
end

class Rake::Task
	include TeamCity
	old_execute = self.instance_method(:execute)
	
	define_method(:execute) do |args|
		teamcity_progress("Executing #{name} rake task") do
			old_execute.bind(self).call(args)
		end
	end
end