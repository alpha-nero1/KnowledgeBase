import React, { Suspense } from 'react';
import { PageLayout } from '../PageLayout';

const DashboardApp = React.lazy(() => import('dashboardApp/App'));

export const DashboardPage = () => (
    <PageLayout>
        <Suspense fallback={<div>Loading...</div>}>
            <DashboardApp />
        </Suspense>
    </PageLayout>
);
