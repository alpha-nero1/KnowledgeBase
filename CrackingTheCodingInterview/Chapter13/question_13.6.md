# 13.6. Object Reflection
Explain what object reflection is in Java and why it is useful.

Reflection in Java is a method in which the Java program is able to introspect itself and manipulate internal properties of the program.

For example it is possible for a Java class to obtain all the names of it's members and display them.

Example:
```
import java.lang.reflect.*;

   public class DumpMethods {
      public static void main(String args[])
      {
         try {
            Class c = Class.forName(args[0]);
            Method m[] = c.getDeclaredMethods();
            for (int i = 0; i < m.length; i++)
            System.out.println(m[i].toString());
         }
         catch (Throwable e) {
            System.err.println(e);
         }
      }
   }
```