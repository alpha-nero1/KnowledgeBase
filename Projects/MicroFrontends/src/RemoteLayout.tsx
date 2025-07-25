import React, { Suspense } from 'react';

const Dashboard = React.lazy(() => import('dashboard/Dashboard'));
const Profile = React.lazy(() => import('profile/Profile'));

export const RemoteLayout = () => (
  <div>
    <h1>Microfrontends Host</h1>
    <Suspense fallback={<div>Loading Dashboard...</div>}>
      <Dashboard />
    </Suspense>
    <Suspense fallback={<div>Loading Profile...</div>}>
      <Profile />
    </Suspense>
  </div>
);
