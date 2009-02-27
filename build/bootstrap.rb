class Configuration
	class << self
		def init(env)
			config = YAML.load_file('settings.yml')[env]
			
			localConfig = YAML.load_file('local-settings.yml') if File.exist?('local-settings.yml')
			config.merge!(localConfig) if localConfig
						
			yield self if block_given?
			materialize(config)
			config.freeze
			config
		end

		private
		def materialize(config)
			config['build_dir_root'] = File.expand_path(config['build_dir'])
			config['build_dir'] = File.join(File.expand_path(config['build_dir']), config['build_configuration'])
			config['source_dir'] = File.expand_path(config['source_dir'])
			config['tools_dir'] = File.expand_path(config['tools_dir'])
			
			config['version_info'] = File.join(config['source_dir'], 'VersionInfo.cs')
			
			config['build_number'] = ENV['BUILD_NUMBER'] || '1.0.0.0' if config['build_number'].nil?
		end
	end
end

environments = %w[development production]

task :configure do |t|
	invoked = false
	t.application.top_level_tasks.each do |task|
		if environments.include?(task)
			Rake::Task[:"#{task}"].invoke 
			invoked = true
		end
	end
	
	Rake::Task[:development].invoke if not invoked
end

environments.each do |env|
	desc "Runs tasks in the #{env} environment" 
	task env do |t|
		$config = Configuration.init(env)
	end
end

# Load default settings. The settings to be loaded can be overriden by specifying an extra task on the command line:
# rake production <other tasks to execute>
Rake::Task[:configure].invoke