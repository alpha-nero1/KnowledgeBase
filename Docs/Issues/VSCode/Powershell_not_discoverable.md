# Powershell not discoverable by VSCode (Windows)
1. Press Win + S and search for Environment Variables.
2. Click "Edit the system environment variables".
3. In the dialog, click Environment Variables.
4. Under System variables, find and edit the Path variable.
4. Click New, then add the path where pwsh.exe or powershell.exe is located. Common locations:

Windows PowerShell:
*In my case this was it:*
`C:\Windows\System32\WindowsPowerShell\v1.0\`

PowerShell Core (v7+):
C:\Program Files\PowerShell\7\