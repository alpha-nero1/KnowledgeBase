# C++

C++ is a compiled programming language, much like java the code must first be compiled before it can be run.

- To comile a .cpp file, exec `g++ <file path>`, g++ will then generate an executable file and you then run the file like you would a script, e.g. `./<executable name>` The executable name will be `a.out` if the `-o` flag is not specified to give the executable a name.
More here on compiling options: https://courses.cs.washington.edu/courses/cse373/99au/unix/g++.html

- Name convention is essentially the same as python! (Cannot use += though).
- `*` means a reference and `**` means a reference to a reference (usually used for arrays).
-- C --
- `strcmp()` is used to compare two strings, outputs 0 if they are the same.
- `strcpy()` copies contents of string2 into string1.
- `malloc()` allocates uninitialised memory.
- `calloc()` allocates uninitialised array block.
- use `const char*` to represent immutable strings. use "string" for mutability.
- use of str-() funcs require casting of `(char*)` becuase that is the type they accept.
- we can define constants using `#define CONSTANT_NAME constant_value`.
- the `free(void *)` function frees a reference from memory!
-- C ---
&nbsp;
-- C++ --
- c++ class structure is like:

```
class MyClass {
  private:
    const char* private_string;
  public:
    const char* public_string;

    MyClass() {
      // Init code.
    }
    ~MyClass() { // Note ~ is a bitwise not. this runs AUTOMATICALLY when the insatnce is out of scope!
      // De-init code.
    }
};
```

- `unsigned long` will not store negative values making the effective range from 0 to `4,294,967,295 (2^32 - 1)`
- instance accessing is done via `->`, e.g `my_class->public_string = "Penner!";`
- the `this` keyword is used but it is not necessary unless there may be a naming clash. so inside `MyClass` it would be fine to write `public_string = "General Kenobi!!"`

- in c++ allocating memory is a little different to what it is in c. in c++ we can simply create variables as normal then use the 'delete' or 'delete[]' keyword to free them up later.
- My key takeaways from the c++ imp is that c++ actually isn't all that bad because you can use constructors and deconstructors and do not need to worry about the allocating and deallocating memory functions directly, although you do need to be weary of the memory allocation.

E.g.

- `g++ -o question-1.1 ./question-1.1.cpp`
- `./question-1.1`


## Setting up C++ projects with CMake
In order to set up projects like you would with pipenv or npm but for C++ you would use CMake.
\
To install CMake: https://cmake.org/install/
\
To set up CMake project: https://cmake.org/cmake/help/v3.16/guide/tutorial/index.html
\
To run CMake project: cd into the \Debug or \Make directory and run the `.exe`
