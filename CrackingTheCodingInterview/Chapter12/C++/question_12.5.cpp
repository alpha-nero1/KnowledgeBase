/*
    12.5. Shallow vs Deep copy?

    A shallow copy copies all the member values from one object to another, A deep copy does
    the same but also copies and pointer objects.
*/

struct Test {
    char * ptr;
}

void shallow_copy(Test & src, Test & dest) {
    dest.ptr = src.ptr;
}

void deep_copy(Test & src, Test & src) {
    dest.ptr = (char *)malloc(strlen(src.ptr) + 1);
    strcpy(dest.ptr, src.ptr);
}

// In most cases you would use a deep copy.
