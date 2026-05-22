# Capstone: Interactive 3D Globe Navigation

Do a dynamic globe of globes animation where tapping one sub globe navigates to another one, animating on a solar curve is the goal.

## Project Overview

Build a **production-quality interactive 3D globe** that synthesizes everything you've learned. This project combines:

- **CustomPaint & Canvas** (Module 08): Complex rendering
- **Physics** (Module 09): Spring animations and momentum
- **Gesture Interaction** (Module 10): Pan, drag, swipe responsiveness
- **3D Transformations & Quaternions** (Module 11): Trackball rotation
- **Performance Optimization** (Module 12): 60fps+ smooth rendering
- **Architecture** (All modules): Clean, reusable components

**Estimated Duration:** 8–12 hours  
**Difficulty:** Expert  
**Outcome:** A portfolio-quality component you can ship

---

## Requirements

### Core Features

**Globe Visualization:**
- [ ] Spherical globe with nodes representing people/connections
- [ ] Nodes positioned evenly on sphere surface
- [ ] Connection lines between nodes
- [ ] Depth-based opacity (nodes behind fade out)
- [ ] Visual halo around root node

**Interaction:**
- [ ] Pan gesture rotates globe smoothly
- [ ] Rotation uses quaternion math (no gimbal lock)
- [ ] Momentum continues rotation after drag release
- [ ] Tap nodes to drill into sub-network

**Navigation:**
- [ ] Multiple globe layers (foreground, background, depth-2)
- [ ] Smooth transitions between zoom levels
- [ ] Back button/gesture to pop navigation stack
- [ ] Animated reveals/dismissals

**Performance:**
- [ ] 60fps during rotation
- [ ] <16ms frame time in profile mode
- [ ] Smooth at 90fps+ on high-refresh devices
- [ ] Responsive to quick touches

**Visual Polish:**
- [ ] Custom curves and easing
- [ ] Material 3 design principles
- [ ] Avatar images (SVG, PNG support)
- [ ] Color harmony and depth cues

---

## Project Structure

```
course/capstone-globe-navigation/
├── README.md (this file)
├── requirements.md (detailed specs)
├── architecture.md (design decisions)
├── implementation-guide.md (step-by-step)
└── reference-code/ (optional examples)
```

---

## Architecture Overview

### Components

1. **Globe Painter** (`CustomPainter`)
   - Renders sphere, nodes, connections
   - Receives pre-loaded images
   - Optimized with `shouldRepaint`

2. **Rotation System**
   - Quaternion accumulation from gestures
   - Converts to Euler angles for projection
   - Smooth trackball interaction

3. **Image Management**
   - Lazy-load SVG → raster conversion
   - Cache loaded images
   - Support future PNG assets

4. **Gesture Handler**
   - Pan updates quaternion
   - Momentum from velocity
   - Tap detection via overlay

5. **Navigation Stack**
   - Multiple globe layers
   - Animated transitions
   - State preservation

---

## Implementation Phases

### Phase 1: Foundation (2 hours)
- [ ] Set up project structure
- [ ] Create data models (MateNode graph)
- [ ] Implement basic sphere positioning
- [ ] Draw nodes on canvas

**Goal:** Static globe rendering

### Phase 2: Rotation & Gestures (2 hours)
- [ ] Implement quaternion math
- [ ] Handle pan gestures
- [ ] Update rotation from input
- [ ] Add momentum animation

**Goal:** Interactive, draggable globe

### Phase 3: Navigation Stack (2 hours)
- [ ] Multi-layer globe system
- [ ] Tap detection via overlay
- [ ] Drill-in animation
- [ ] Back navigation

**Goal:** Navigable, layered experience

### Phase 4: Polish & Performance (2-3 hours)
- [ ] Image loading system
- [ ] Avatar rendering on canvas
- [ ] Optimize frame rendering
- [ ] Visual refinements

**Goal:** Production-ready component

---

## Key Algorithms

### Spherical Positioning

Convert spherical coordinates (latitude/longitude) to 3D Cartesian:

```
x = r * sin(latitude) * cos(longitude)
y = r * sin(latitude) * sin(longitude)
z = r * cos(latitude)
```

Then project to 2D screen space with rotation.

### Quaternion Rotation

From drag delta to quaternion:

```
1. Calculate drag vector in screen space
2. Convert to 3D axis in world space
3. Create quaternion from axis-angle
4. Multiply with current quaternion
5. Convert back to matrix for rendering
```

### Depth Sorting

For correct overlap rendering:

```
1. Project all nodes to 2D
2. Calculate Z-depth for each
3. Sort by depth (far to near)
4. Render in order
5. Overlay interactive hitboxes in depth order
```

---

## Performance Targets

| Metric | Target | Tool |
|--------|--------|------|
| Frame time | <16ms | DevTools Perf |
| Frame rate | 60fps | Visual inspection |
| High-refresh | 90fps+ | On 120Hz device |
| Load time | <2s | DevTools network |
| Memory | <100MB | DevTools Memory |

---

## Testing Checklist

### Functional
- [ ] Rotation works in all directions
- [ ] Nodes position correctly on sphere
- [ ] Connections render between correct nodes
- [ ] Tap detection works for all nodes
- [ ] Navigation stack handles 3+ levels
- [ ] Back button dismisses correctly

### Performance
- [ ] No frame drops during rapid rotation
- [ ] Smooth at 60fps minimum
- [ ] No jank on list scroll
- [ ] Image loading doesn't block UI
- [ ] Memory stable over time (no leaks)

### Visual
- [ ] Depth opacity creates 3D effect
- [ ] Curves feel natural
- [ ] Colors are harmonious
- [ ] Text readable on all backgrounds
- [ ] Animations smooth and purposeful

### Edge Cases
- [ ] Handle empty network
- [ ] Handle single-node network
- [ ] Large network (100+ nodes)
- [ ] Rapid gesture sequences
- [ ] Screen rotation while animating

---

## Capstone Learning Goals

Upon completion, you will have demonstrated:

✅ **Canvas Mastery**
- Complex rendering at scale
- Optimization with shouldRepaint
- Depth sorting and layering

✅ **Animation Expertise**
- Multiple concurrent animations
- Gesture-responsive timing
- Physics-based momentum

✅ **3D Mathematics**
- Quaternion operations
- Matrix transformations
- Coordinate system conversions

✅ **Architecture**
- Separation of concerns
- Reusable components
- Clean code patterns

✅ **Performance Discipline**
- Profiling and optimization
- Memory management
- Frame timing awareness

---

## Resources & References

### Quaternion Math
- [Quaternion on Wikipedia](https://en.wikipedia.org/wiki/Quaternion)
- [Trackball Camera](https://en.wikibooks.org/wiki/OpenGL_Programming/Modern_OpenGL_Tutorial_Arcball)

### Projection Math
- [3D Graphics Primers](https://learnopengl.com/)
- [Screen Space Projection](https://en.wikipedia.org/wiki/Orthographic_projection)

### Flutter Specifics
- [Matrix4 API](https://api.flutter.dev/flutter/vector_math_64/Matrix4-class.html)
- [CustomPaint Optimization](https://flutter.dev/docs/perf/rendering/ui-performance)

### Similar Projects
- Examine Flutter Gallery for CustomPaint examples
- Study "3D Graphics in Flutter" articles
- Reference production globe visualizations

---

## Extension Ideas

Once complete, consider extending:

- **Particle effects** around selected nodes
- **Physics simulation** for node interactions
- **Search/filter** nodes by name
- **Data visualization** (node size = importance)
- **Animations** for data updates
- **Export** globe visualization as image
- **VR/AR mode** (experimental)

---

## Success Criteria

**You're done when:**

1. ✅ Globe renders smoothly (60fps)
2. ✅ Rotation feels responsive and natural
3. ✅ Tap navigation works correctly
4. ✅ Images load and display properly
5. ✅ Code is clean and documented
6. ✅ You can explain every animation decision
7. ✅ You're proud to show it to others

---

## Next Steps

1. **Read** `architecture.md` to understand design decisions
2. **Review** `implementation-guide.md` for step-by-step walkthrough
3. **Start** Phase 1 (foundation)
4. **Profile** continuously in Phase 4
5. **Refactor** for clarity and performance
6. **Ship** a production-quality component

---

## Questions to Ask Yourself

- "Why this easing curve and not another?"
- "Could I batch these animations?"
- "Am I doing too much in paint()?"
- "Is this gesture responsive enough?"
- "Would 100 nodes still animate at 60fps?"
- "Can I explain the quaternion math?"

Answering these deeply means you've truly mastered flutter animations.

---

## Showcase

Once complete:
- Add to your portfolio
- Open-source on GitHub
- Write a blog post about the implementation
- Share on Flutter community
- Use as reference for future projects

---

## Conclusion

This capstone synthesizes all 12 modules into a single, professional-grade component. The skills you demonstrate here—advanced rendering, gesture responsiveness, 3D math, performance optimization—are exactly what separates junior developers from senior animation experts in Flutter.

**You've got this.** Build something amazing.

---

*Last Updated: 2026*
*For feedback or suggestions, submit issues or PRs*
