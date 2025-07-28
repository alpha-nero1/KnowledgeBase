import React, { Suspense } from 'react';
import { PageLayout } from '../PageLayout';

const ProfileApp = React.lazy(() => import('profileApp/App'));

export const ProfilePage = () => (
    <PageLayout>
        <Suspense fallback={<div>Loading...</div>}>
            <ProfileApp />
        </Suspense>
    </PageLayout>
);