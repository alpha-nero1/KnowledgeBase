# C and C++
It's good to have at least a fundamental understanding of C and C++ because of it's use in low level
programming and it's speed. Many would also call it an OG language.

## Classes and Inheritance.
The code bellow demonstrates implementation of basic class with inheritance:
```
#include <iostream>
using namespace std;

#define NAME_SIZE 50; // Defines a macro.

class Person {
    int d; // all members are private by default.
    char name[NAME_SIZE]; // String.

    public:
        void aboutMe() {
            cout << "I am a person";
        }
}

class Student : public Person {
    public:
        void aboutMe() {
            cout << "Ich bin ein Student";
        }
}

int main() {
    Student * p = new Student();
    p->aboutMe(); // Prints "Ich bin ein Student"
    delete p; // "delete" key cleans up allocated memory!
    return 0;
}
```

## Constructors and Deconstructors
Constructor is called upon object creation. If none is specified, then the compiler will create a default.
If you just need to init primitive types then this is acceptable:
```
class Person {
    public:
        Person(int a) {
            id = a;
        }
}
```

The above works for primitive types, but if you wanted to pass complex types in you may need to do
```
class Person {
    public:
        Person(int a) : id(a) {
        }
}
```

The data member id is assigned before the actual object is created and before the remainder of constructor code
is called, this approach is necessary when the fields are constants or class types.

destructors are defined as:
```
class Person {
    public:
        ~Person() {
            delete obj;
        }
}
```

## Virtual functions.
If we instantiated person like this:
```
Person * p = new Student();
p->aboutMe()
```

This would print "I am a person" and not the student text, why? this is because aboutMe() is
resolved at **compile time** and is expecting to call the function on Person class. in order to avoid this
scenario we can define the p var correctly but we can also make the Person aboutMe() function *virtual*

Virtual says, hey, resolve the sublcass function before ever coming to me.

Also we can use the `virtual` keyword to define functions that subclasses MUST impement because
implementation does not make sense in the base class.

### Virtual destructors.
We need to be carefull about similar scenarios occuring with the destructors, say we defined the var as such
in the previous section, the desctructor ~Person() would be executed and not the correct ~Student() func.
however ~Student() may have contained critical memory cleanup!!

In the case we want to make sure subclassed destructors are called we should also make our base
class destructor virtual too, like:
```
class Person {
    public:
        virtual ~Person() {
            cout << "Deleting person" << endl;
        }
}
```

## Pointers and References
### Pointer
A pointer holds the address of a variable and can be used to perform an operation that could be done on the variable such as accessing it and modifying it.

Two pointers can equal eachother, such that changing ones value changes the other because they point to the same address.

```
int * p = new int;
*p = 7;
int * q = p;
*p = 8
cout << *q; // Prints 8
```

### References
A reference is another name for an alias to a pre-existing object. It does not have memory of its own:

```
int a = 5;
int & b = a;
b = 7;
cout << a; // prints 7
```

** Unlike pointers, references cannot be null and cannot be reassigned to another piece of memory **

## Templates
Similar to generics

```
template <class T>class ShiftedList {
    ...
}
```