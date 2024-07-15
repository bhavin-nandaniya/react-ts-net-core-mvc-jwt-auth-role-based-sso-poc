// context/AuthContext.tsx
import React, { createContext, useContext, useState } from "react";
import { AuthData } from "../types/AuthTypes"; // Ensure AuthData is properly defined

interface AuthContextType {
    auth: AuthData | null;
    setAuth: (data: AuthData | null) => void;
}

const AuthContext = createContext<AuthContextType>({
    auth: null,
    setAuth: () => {},
});

export const AuthProvider: React.FC = ({ children }) => {
    const storedAuth = localStorage.getItem('auth');
    const [auth, setAuth] = useState<AuthData | null>(storedAuth ? JSON.parse(storedAuth) : null);

    return (
        <AuthContext.Provider value={{ auth, setAuth }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = (): AuthContextType => useContext(AuthContext);

export default AuthContext;
