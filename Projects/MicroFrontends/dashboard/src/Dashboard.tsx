import React from 'react';
// @ts-ignore
import { Button } from '@shared-ui';

const Dashboard: React.FC = () => {
  return (
    <div>
      <h2>Dashboard Microfrontend</h2>
      <Button onClick={() => alert('Dashboard Button Clicked!')}>Dashboard Action</Button>
    </div>
  );
};

export default Dashboard;
