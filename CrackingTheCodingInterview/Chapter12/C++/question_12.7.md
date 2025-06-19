# 12.7. Virtual Base Class.
Why does the deconstructor in base class need to be declared virtual.

if we don't declare the constructor e.g. like so:
```
virtual ~BaseType() {

}
```

then the base class constructor will be called instead of any subclass deconstructor if defined like so:

```
BaseType * baseType = new BaseType();
BaseType * extendedType = new ExtendedType();
```

ExtendedType may contain extra memory cleanup code that MUST run in order to prevent a memory leak.
If the BaseClass is not virtual then it's deconstructor will be called every time, not that of
ExtendedType.