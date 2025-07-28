# 📦 Projects / ReactLibrary
## 🤷‍♂️ What does it do?
This project demonstrates how to create a React component library from scratch. 

It includes three reusable UI components (Button, Input, Card) that can be imported and used in other React applications. 

The library is built with modern tooling (rollup) and follows best practices for component library development (jest + storybook).

## 👷 How does it work?
The project uses several key technologies working together:

- **🏗️ Rollup** - Bundles the library into multiple formats (CommonJS & ES modules) with tree-shaking
- **🎨 PostCSS** - Processes and extracts CSS styles into separate files
- **� Jest + React Testing Library** - Comprehensive testing with 20+ test cases
- **📚 Storybook** - Interactive documentation and component playground
- **🔧 Babel** - Transforms modern JavaScript/TypeScript for compatibility

The build process creates a `dist/` folder with:
- `index.js` (CommonJS format)
- `index.esm.js` (ES Module format) 
- `index.d.ts` (TypeScript definitions)
- `index.css` (Extracted styles)

## 🛠️ Project setup
```bash
# Install dependencies
npm install

# Build the library
npm run build

# Run tests
npm test

# Start Storybook (documentation)
npm run storybook
```

## 🏎️ How to run
**Development Mode:**
```bash
npm run dev          # Watch mode with auto-rebuild
npm run test:watch   # Watch mode for tests
npm run storybook    # Start Storybook at http://localhost:6006
```

**Using the Library:**
After building, the library can be imported in other projects:
```tsx
import { Button, Input, Card } from 'react-library';

function App() {
  return (
    <Card title="Welcome" padding="large">
      <Input label="Email" type="email" />
      <Button variant="primary" onClick={() => alert('Hello!')}>
        Submit
      </Button>
    </Card>
  );
}
```

## ⚖️ Final Remarks
This project showcases modern React library development patterns including:
- ✅ **Multi-format builds** for maximum compatibility
- ✅ **Complete TypeScript support** with auto-generated definitions  
- ✅ **Comprehensive testing** with Jest and React Testing Library
- ✅ **Interactive documentation** with Storybook!
- ✅ **CSS processing** with PostCSS and extraction
- ✅ **Professional tooling** with Rollup, Babel, and modern configs

The setup demonstrates how to create a library that's ready for npm publishing and can be consumed by any React application with full TypeScript support and tree-shaking capabilities.