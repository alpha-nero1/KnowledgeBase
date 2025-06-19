/*
    12.9. Smart Pointer.
    Write a smart pointer class. A smart pointer is a data type, usually implemented
    with templates, that simulates a pointer while also providing automatic garbage collection. It
    automatically counts the number of references to a SmartPointer<T*> object and frees the
    object of type T when the reference count hits zero.
*/

#include <iostream>
using namespace std;

// A generic smart pointer class
template <class T> class SmartPtr {
    // Actual pointer
    T* ptr;

    public:
        // Constructor
        explicit SmartPtr(T* p = NULL) { ptr = p; }

        // Destructor
        ~SmartPtr() { delete (ptr); }

        // Overloading dereferencing operator
        T& operator*() { return *ptr; }

        // Overloading arrow operator so that
        // members of T can be accessed
        // like a pointer (useful if T represents
        // a class or struct or union type)
        T* operator->() { return ptr; }
};
