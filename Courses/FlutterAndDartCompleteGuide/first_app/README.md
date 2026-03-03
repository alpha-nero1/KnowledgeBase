# first_app

Demo app.

Flutter widget catalog: https://docs.flutter.dev/ui/widgets

`const` tells the dart compiler that this widget NEVER changes, it optimises the runtime performance of the application - if you mark something as const, only one memory reference of that widget will be created no matter how many times the same widget is defied.

`Widget` is essentially a component but don't tell anyone, this is similar to react class components but without the overhead of JSX.