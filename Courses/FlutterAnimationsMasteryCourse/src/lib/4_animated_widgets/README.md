# Animated widgets

## What are they?
Animated widgets remove the tedious need for AnimatedBuilder, AnimationController and Tween setup by providing a single `Animated...` widget which takes care of all of this under the hood for you!

## Examples
| Widget | Animates |
|--------|----------|
| `AnimatedOpacity` | opacity |
| `AnimatedScale` | scale transform |
| `AnimatedRotation` | rotation angle |
| `AnimatedPositioned` | position (in Stack) |
| `AnimatedAlign` | alignment |
| `AnimatedContainer` | size, color, border, shadow, etc. |
| `AnimatedCrossFade` | cross-fade between two widgets |
| `AnimatedSwitcher` | animated widget replacement |
| `AnimatedDefaultTextStyle` | text style (color, size, weight) |
| `AnimatedPadding` | padding |
| `AnimatedSlide` | slide position |

## So what is implicit and explicit animations then?
*Explicit* animations are ANY time you defined the animation explicitly using AnimationController, AnimatedBuilder & Tween
---
*Implicit* animations are ANY time you just defined a value that any `Animated*` widget simply listens to in order to animate it's contents.

## Repeated implicit animations
You will need to use Timer.periodic() to increment your values!

## AnimatedDefaultTextStyle
There is a widget for changing the default text style of a page! unreal!
