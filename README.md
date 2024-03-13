# Hidden Command

Hidden Command can execute command without displaying a console window (unlike traditional BAT scripts).

Features that should be included:
X Must have logging framework (NLog) and log to console.log file
X Executing commands directly via arguments (example: hCMD start /affinity 1 notepad)
- Executing commands via profile/file (hCMD /profile OpenNotepad)
X NOTE! Make ProcessExecutor with Singleton design pattern
- Can include itself into environment PATH variable
- Think of additional features that could be useful
- A way to show usage instructions