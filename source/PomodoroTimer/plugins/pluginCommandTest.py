from PomodoroTimer.Plugin import PluginCommandBase

print "Loading PluginTestCommand"

class PluginTestCommand(PluginCommandBase):
    def __new__():
        #self.outputStream = outputStream
        print "creating PluginTestCommand"

	def Execute(self):
		outputStream.Write("PluginTestCommand called\r\n")
		
#testCommand = PluginTestCommand()
#testCommand.outputStream = IoC.Resolve[IOutputStream]()
#testCommand = IoC.Resolve[PluginTestCommand]()
#Commands.AddCommand(testCommand)