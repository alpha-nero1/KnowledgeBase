/*
  12.4. How do virtual functions work in c++?

  Virtual functions rely on a "vtable" (Virtual Table). If any function or a class is declared to be
  virtual, a vtable is constructed which stores addresses of the virtual functions of the class.

  The compiler also adds a hidden vptr variable in all such classes which points to the vtable of that class.
  if the virtual function is not overriden by the subclass, the vtable of the child class stores the address of the
  function of it's parent, this way it knows to call it.

  Essentially child class functions can be "virtually" mapped to the parent.

  Dynamic binding in C++ is performed through the vtable mechanism.
*/

class Shape {
    public:
        int edge_length;
        virtual int circumference() {
            return 0;
        }
}

class Triangle : public Shape {
    public:
        int circumference() {
            return 3 * edge_length;
        }
}

int main() {
    Shape * x = new Shape();
    x->circumference(); // Resolves the Shape class function.
    Shape * y = new Triangle();
    y->circumference(); // Resolves the Triangle class function. But it would not if virtual on Shape was not specified.

}