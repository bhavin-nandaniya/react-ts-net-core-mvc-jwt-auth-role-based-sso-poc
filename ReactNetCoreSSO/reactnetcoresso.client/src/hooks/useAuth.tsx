// hooks/useAuth.ts
import { useContext, useEffect } from 'react';
import AuthContext from '../context/AuthContext';

const useAuth = () => {
    const { auth, setAuth } = useContext(AuthContext);

    // Check localStorage for token during initialization
    useEffect(() => {
        const storedAuth = localStorage.getItem('auth');
        if (storedAuth) {
            setAuth(JSON.parse(storedAuth));
        }
    }, [setAuth]);

    // Update localStorage whenever auth changes
    useEffect(() => {
        localStorage.setItem('auth', JSON.stringify(auth));
    }, [auth]);

    return { auth, setAuth };
};

export default useAuth;
