import { useMemo } from "react";
import { Link } from 'react-router-dom';

interface ILayoutItem {
    key: string;
    title: string;
    link: string;
}

interface IBodyLayoutProps {
    children: React.ReactNode;
}

/**
 * Static definition of the layout items.
 */
const layout: ILayoutItem[] = [
    { key: 'overview', title: 'Overview', link: '/' },
    { key: 'dashboard', title: 'Dashboard', link: '/dashboard' },
    { key: 'profile', title: 'Profile', link: '/profile' },
];

export const PageLayout = ({ children }: IBodyLayoutProps) => {
    const navItems = useMemo(() => layout.map(layoutItem => 
        <div key={layoutItem.key} style={{ margin: '10px 0' }}>
            <Link to={layoutItem.link}>{layoutItem.title}</Link>
        </div>
    ), []);

    return (
        <div style={{ 
            display: 'flex', 
            height: '100vh', 
            width: '100vw',
            flexDirection: 'row' 
        }}>
            <ul style={{ flex: 1, display: 'flex', flexDirection: 'column', listStyleType: 'none', padding: 0, alignItems: 'flex-start' }}>
                {navItems}
            </ul>
            <div style={{ flex: 3 }}>
                {children}
            </div>
        </div>
    );
}
