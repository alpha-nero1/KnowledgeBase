# 12.6. Volatile
What is the significance of the keyword `volatile` in C?

The keyword `volatile` in C indicates to the comiler that the variable can change from outside the programs control, (without any update done from the code). This could be done by the OS, another thread, or the hardware.

Because the value can unexpectedly change, the compiler will reload the value each time from memory (when accessed)
