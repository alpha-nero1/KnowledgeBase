const baseUri = 'http://localhost:5000';

export const Api = () => {
    const login = async (username: string, password: string): Promise<{ token: string }> => {
        const response = await fetch(`${baseUri}/login?username=${username}&password=${password}`, {
            method: 'POST'
        });

        if (!response.ok) {
            throw new Error('Login failed');
        }

        const data = await response.json();
        return data;
    }

    return {
        login
    }
}