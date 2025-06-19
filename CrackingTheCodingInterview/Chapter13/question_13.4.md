# 13.4. Generics vs. Templates.
Explain the difference between templates in C ++ and generics in Java.

- Java does not create a new copy of the template code per type, c++ does, this means that in Java you can access the same static variable on types MyType<A> and MyType<B> but in CPP you cannot.
- C++ templates can use primitive types whilst Java cannot.
- In Java you can restrict that the generic type be of a certain type.
- In Java, the type paramater cannot be used for static methods and variables. Whilst in C++ you can because they are infact different types.
- In Java all instances of MyType are the same type regardless of paramter, types are erased when compiled, casting is replaced in it's place. This is refered to as syntactical sugar.