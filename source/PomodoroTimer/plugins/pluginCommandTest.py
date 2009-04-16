from PomodoroTimer.Plugin import ScriptCommandBase
from PomodoroTimer.Plugin import IOutputStream

print "loading Script"

class TestCommand(ScriptCommandBase):
    def __init__(self):
        self.outputStream = IoC.Resolve[IOutputStream]()
        self.outputStream.Write("creating PluginTestCommand\r\n")

    def Execute(self):
		self.outputStream.Write("executing Command\r\n")

    def GetName(self):
        return "TestCommand"

		
#testCommand = PluginTestCommand()
#testCommand.outputStream = 
#testCommand = IoC.Resolve[PluginTestCommand]()
#Commands.AddCommand(testCommand)