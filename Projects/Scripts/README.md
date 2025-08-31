# 📜 Projects / Scripts
> *⚠️The code in this project is NOT best practice; for demonstration purposes ONLY*

## 🤷‍♂️ What does it do?
This project is a collection of useful JavaScript scripts used for varying purposes and common use cases.

## 👷 How does it work?
See script by script if the code is directly invoked, othrwise you may need to uncomment the invocation code.

When the script is ready just run `node <scriptname>.js`

Scripts include:
- `apiTest.js` - generic API tester implementation using JS
- `codeBreakdown.js` - requires installation of cloc but gives a breakdown of how your repo looks like code wise (what langauges are used the most)
- `encryptFile.js` - encrypt /decrypt files on your machine.
- `spawnMultuProcess.js` - Allows you to spin up multiple processes in unison and handle graceful shutdown.
- `systemMonitor.js` - Really cool script that handles OS system monitoring and even spins up a small node server to monitor it!

## ⚖️ Final Remarks
There are many really cool and amazing script in this project, the premise being that if I ever need to implement a script I can look to here for inspiration.

Things to note:
- Not once do I use classes in JS, classes in JS suck as they always loose the 'this' binding, classes are just functions under the hood anyway so I skip the troublesome syntactical sugar bit. JS just not build for classes IMO.
- Closures allow me to manage state and effectively replace the need for 'this'
