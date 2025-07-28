import './App.css'
import { OverviewPage } from './pages/overviewPage';
import { ProfilePage } from './pages/profilePage';
import { DashboardPage } from './pages/dashboardPage';
import { BrowserRouter, Routes, Route } from 'react-router-dom'

const App = () => (
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<OverviewPage />} />
      <Route path="/dashboard" element={<DashboardPage />} />
      <Route path="/profile" element={<ProfilePage />} />
    </Routes>
  </BrowserRouter>
);

export default App
