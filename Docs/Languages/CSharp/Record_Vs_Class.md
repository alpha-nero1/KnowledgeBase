# Record vs Class, what's the dealio?
Out of the box `record` gives you:
- `ToString()`
- `Equals()`
- `GetHashCode()`

Which operate based on the values you have given it. And that's the main point, a `record` is a values based object.

## ✅ Use record when your object is:
- An immutable data structure (like an event or dto).
- intended to be compared by inside values not identity.
- needs a clean presentable `ToString()` GREAT FOR LOGGING!


## 🔁 Key differences
### Class
```
var w1 = new WeatherUpdated { City = "Sydney", Temperature = 21.0m };
var w2 = new WeatherUpdated { City = "Sydney", Temperature = 21.0m };

Console.WriteLine(w1 == w2); // false! Reference equality
Console.WriteLine(w1);       // Just the type name (no property printout)
```

### Record
```
var w1 = new WeatherUpdated("Sydney", 21.0m);
var w2 = new WeatherUpdated("Sydney", 21.0m);

Console.WriteLine(w1 == w2); // true! Value equality
Console.WriteLine(w1);       // WeatherUpdated { City = Sydney, Temperature = 21.0 }
```